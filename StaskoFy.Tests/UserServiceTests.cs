using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using StaskoFy.Core.Service;
using StaskoFy.DataAccess;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.Models.Enums;
using StaskoFy.ViewModels.Playlist;

namespace StaskoFy.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private StaskoFyDbContext context;
        private UserManager<User> userManager;
        private Repository<Artist> artistRepo;
        private UserService userService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<StaskoFyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new StaskoFyDbContext(options);

            // Create a real UserManager with the InMemory store
            var userStore = new Microsoft.AspNetCore.Identity.EntityFrameworkCore.UserStore<User, IdentityRole<Guid>, StaskoFyDbContext, Guid>(context);
            var optionsAccessor = new Mock<IOptions<IdentityOptions>>();
            optionsAccessor.Setup(o => o.Value).Returns(new IdentityOptions());
            var passwordHasher = new PasswordHasher<User>();
            var userValidators = new List<IUserValidator<User>>();
            var passwordValidators = new List<IPasswordValidator<User>>();
            var lookupNormalizer = new UpperInvariantLookupNormalizer();
            var errorDescriber = new IdentityErrorDescriber();
            var logger = new Mock<ILogger<UserManager<User>>>();

            userManager = new UserManager<User>(
                userStore,
                optionsAccessor.Object,
                passwordHasher,
                userValidators,
                passwordValidators,
                lookupNormalizer,
                errorDescriber,
                null,
                logger.Object
            );

            artistRepo = new Repository<Artist>(context);
            userService = new UserService(userManager, artistRepo);
        }

        [TearDown]
        public void TearDown()
        {
            userManager.Dispose();
            context.Dispose();
        }

        private async Task<User> CreateUser(string username, string email)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = username,
                Email = email,
                ImageURL = $"/img/{username}.png",
                CloudinaryPublicId = "test-pub"
            };
            await userManager.CreateAsync(user, "TestPass123!");
            return user;
        }

        // ── GetTotalPagesAsync ──

        [Test]
        public async Task GetTotalPagesAsync_ReturnsCorrectPageCount()
        {
            for (int i = 0; i < 10; i++)
                await CreateUser($"User{i}", $"user{i}@test.com");

            var result = await userService.GetTotalPagesAsync(8);

            Assert.That(result, Is.EqualTo(2)); // 10 / 8 = 1.25 → ceil = 2
        }

        [Test]
        public async Task GetTotalPagesAsync_ReturnsZeroWhenNoUsers()
        {
            var result = await userService.GetTotalPagesAsync(8);

            Assert.That(result, Is.EqualTo(0));
        }

        // ── FilteredUsersWithoutAdminAsync ──

        [Test]
        public async Task FilteredUsersWithoutAdminAsync_ExcludesAdminAndCurrentUser()
        {
            var admin = await CreateUser("AdminUser", "admin@test.com");
            var currentUser = await CreateUser("CurrentUser", "current@test.com");
            var regularUser = await CreateUser("RegularUser", "regular@test.com");

            var result = (await userService.FilteredUsersWithoutAdminAsync(admin.Id, currentUser.Id, "", 1, 10)).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Username, Is.EqualTo("RegularUser"));
        }

        [Test]
        public async Task FilteredUsersWithoutAdminAsync_FiltersByUsername()
        {
            var admin = await CreateUser("AdminUser", "admin@test.com");
            var user1 = await CreateUser("JohnDoe", "john@test.com");
            var user2 = await CreateUser("JaneDoe", "jane@test.com");
            var user3 = await CreateUser("BobSmith", "bob@test.com");

            var result = (await userService.FilteredUsersWithoutAdminAsync(admin.Id, Guid.NewGuid(), "Doe", 1, 10)).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task FilteredUsersWithoutAdminAsync_RespectsPagination()
        {
            var admin = await CreateUser("AdminUser", "admin@test.com");

            for (int i = 0; i < 10; i++)
                await CreateUser($"User{i:D2}", $"user{i}@test.com");

            var page1 = (await userService.FilteredUsersWithoutAdminAsync(admin.Id, Guid.NewGuid(), "", 1, 4)).ToList();
            var page2 = (await userService.FilteredUsersWithoutAdminAsync(admin.Id, Guid.NewGuid(), "", 2, 4)).ToList();

            Assert.That(page1.Count, Is.EqualTo(4));
            Assert.That(page2.Count, Is.EqualTo(4));
        }

        // ── GetUserWithPlaylistsByIdAsync ──

        [Test]
        public async Task GetUserWithPlaylistsByIdAsync_ReturnsUserWithPlaylists()
        {
            var user = await CreateUser("PlaylistUser", "playlist@test.com");

            // Add playlists directly
            context.Playlists.Add(new Playlist
            {
                Id = Guid.NewGuid(),
                Title = "My Playlist",
                UserId = user.Id,
                DateCreated = DateOnly.FromDateTime(DateTime.Now),
                ImageURL = "/img/pl.png",
                CloudinaryPublicId = "pl-pub",
                IsPublic = true
            });
            await context.SaveChangesAsync();

            var result = await userService.GetUserWithPlaylistsByIdAsync(user.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Username, Is.EqualTo("PlaylistUser"));
            Assert.That(result.Playlists.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetUserWithPlaylistsByIdAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await userService.GetUserWithPlaylistsByIdAsync(Guid.NewGuid()));
        }

        // ── IsUserArtistAsync ──

        [Test]
        public async Task IsUserArtistAsync_ReturnsTrueWhenUserIsArtist()
        {
            var user = await CreateUser("ArtistUser", "artist@test.com");
            await artistRepo.AddAsync(new Artist { Id = Guid.NewGuid(), UserId = user.Id, IsAccepted = UploadStatus.Approved });

            var result = await userService.IsUserArtistAsync(user.Id);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task IsUserArtistAsync_ReturnsFalseWhenUserIsNotArtist()
        {
            var user = await CreateUser("RegularUser", "reg@test.com");

            var result = await userService.IsUserArtistAsync(user.Id);

            Assert.That(result, Is.False);
        }
    }
}
