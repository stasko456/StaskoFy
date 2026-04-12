using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StaskoFy.Core.Service;
using StaskoFy.DataAccess;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.Models.Enums;
using StaskoFy.ViewModels.Artist;

namespace StaskoFy.Tests
{
    [TestFixture]
    public class ArtistServiceTests
    {
        private StaskoFyDbContext context;
        private Repository<Artist> artistRepo;
        private ArtistService artistService;

        private User testUser1;
        private User testUser2;
        private User testUser3;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<StaskoFyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new StaskoFyDbContext(options);
            artistRepo = new Repository<Artist>(context);
            artistService = new ArtistService(artistRepo);

            // Seed users
            testUser1 = new User { Id = Guid.NewGuid(), UserName = "ArtistOne", Email = "one@test.com", ImageURL = "/img/1.png" };
            testUser2 = new User { Id = Guid.NewGuid(), UserName = "ArtistTwo", Email = "two@test.com", ImageURL = "/img/2.png" };
            testUser3 = new User { Id = Guid.NewGuid(), UserName = "ThirdUser", Email = "three@test.com", ImageURL = "/img/3.png" };

            context.Users.AddRange(testUser1, testUser2, testUser3);
            await context.SaveChangesAsync();
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        // ── GetFilteredArtistsAsync ──

        [Test]
        public async Task GetFilteredArtistsAsync_ExcludesCurrentUser()
        {
            await artistRepo.AddAsync(new Artist { Id = Guid.NewGuid(), UserId = testUser1.Id, IsAccepted = UploadStatus.Approved });
            await artistRepo.AddAsync(new Artist { Id = Guid.NewGuid(), UserId = testUser2.Id, IsAccepted = UploadStatus.Approved });

            var result = (await artistService.GetFilteredArtistsAsync(testUser1.Id, "")).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Username, Is.EqualTo("ArtistTwo"));
        }

        [Test]
        public async Task GetFilteredArtistsAsync_FiltersByUsername()
        {
            await artistRepo.AddAsync(new Artist { Id = Guid.NewGuid(), UserId = testUser1.Id, IsAccepted = UploadStatus.Approved });
            await artistRepo.AddAsync(new Artist { Id = Guid.NewGuid(), UserId = testUser2.Id, IsAccepted = UploadStatus.Approved });
            await artistRepo.AddAsync(new Artist { Id = Guid.NewGuid(), UserId = testUser3.Id, IsAccepted = UploadStatus.Approved });

            var result = (await artistService.GetFilteredArtistsAsync(Guid.NewGuid(), "Artist")).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        // ── GetArtistByIdAsync ──

        [Test]
        public async Task GetArtistByIdAsync_ReturnsArtistWhenFound()
        {
            var artistId = Guid.NewGuid();
            await artistRepo.AddAsync(new Artist { Id = artistId, UserId = testUser1.Id, IsAccepted = UploadStatus.Approved });

            var result = await artistService.GetArtistByIdAsync(artistId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Username, Is.EqualTo("ArtistOne"));
        }

        [Test]
        public void GetArtistByIdAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await artistService.GetArtistByIdAsync(Guid.NewGuid()));
        }

        // ── AddArtistAsync ──

        [Test]
        public async Task AddArtistAsync_CreatesArtist()
        {
            var model = new ArtistCreateViewModel { UserId = testUser1.Id };

            await artistService.AddArtistAsync(model);

            var artists = await artistRepo.GetAllAttached().ToListAsync();
            Assert.That(artists.Count, Is.EqualTo(1));
            Assert.That(artists[0].UserId, Is.EqualTo(testUser1.Id));
        }

        // ── RemoveArtistAsync ──

        [Test]
        public async Task RemoveArtistAsync_RemovesArtist()
        {
            var artistId = Guid.NewGuid();
            await artistRepo.AddAsync(new Artist { Id = artistId, UserId = testUser1.Id, IsAccepted = UploadStatus.Approved });

            await artistService.RemoveArtistAsync(artistId);

            var result = await artistRepo.GetByIdAsync(artistId);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void RemoveArtistAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await artistService.RemoveArtistAsync(Guid.NewGuid()));
        }

        // ── PopulateArtistSelectListAsync ──

        [Test]
        public async Task PopulateArtistSelectListAsync_ExcludesCurrentUser()
        {
            await artistRepo.AddAsync(new Artist { Id = Guid.NewGuid(), UserId = testUser1.Id, IsAccepted = UploadStatus.Approved });
            await artistRepo.AddAsync(new Artist { Id = Guid.NewGuid(), UserId = testUser2.Id, IsAccepted = UploadStatus.Approved });

            var result = (await artistService.PopulateArtistSelectListAsync(testUser1.Id)).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Username, Is.EqualTo("ArtistTwo"));
        }

        // ── GetArtistByIdWithProjectsAsync ──

        [Test]
        public async Task GetArtistByIdWithProjectsAsync_ReturnsArtistWithProjects()
        {
            var artistId = Guid.NewGuid();
            var artist = new Artist { Id = artistId, UserId = testUser1.Id, IsAccepted = UploadStatus.Approved };
            await artistRepo.AddAsync(artist);

            var result = await artistService.GetArtistByIdWithProjectsAsync(testUser1.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Username, Is.EqualTo("ArtistOne"));
        }

        [Test]
        public void GetArtistByIdWithProjectsAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await artistService.GetArtistByIdWithProjectsAsync(Guid.NewGuid()));
        }

        // ── GetTotalPendingPagesAsync ──

        [Test]
        public async Task GetTotalPendingPagesAsync_ReturnsCorrectPageCount()
        {
            for (int i = 0; i < 7; i++)
            {
                await artistRepo.AddAsync(new Artist { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), IsAccepted = UploadStatus.Pending });
            }
            await artistRepo.AddAsync(new Artist { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), IsAccepted = UploadStatus.Approved });

            var result = await artistService.GetTotalPendingPagesAsync(5);

            Assert.That(result, Is.EqualTo(2)); // 7 pending / 5 = 1.4 → ceil = 2
        }

        // ── FilterArtistsWithPendingStatusAsync ──

        [Test]
        public async Task FilterArtistsWithPendingStatusAsync_ReturnsNonApprovedArtists()
        {
            await artistRepo.AddAsync(new Artist { Id = Guid.NewGuid(), UserId = testUser1.Id, IsAccepted = UploadStatus.Pending });
            await artistRepo.AddAsync(new Artist { Id = Guid.NewGuid(), UserId = testUser2.Id, IsAccepted = UploadStatus.Approved });

            var result = (await artistService.FilterArtistsWithPendingStatusAsync("", 1, 10)).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].IsAccepted, Is.EqualTo(UploadStatus.Pending));
        }

        [Test]
        public async Task FilterArtistsWithPendingStatusAsync_FiltersByName()
        {
            await artistRepo.AddAsync(new Artist { Id = Guid.NewGuid(), UserId = testUser1.Id, IsAccepted = UploadStatus.Pending });
            await artistRepo.AddAsync(new Artist { Id = Guid.NewGuid(), UserId = testUser3.Id, IsAccepted = UploadStatus.Pending });

            var result = (await artistService.FilterArtistsWithPendingStatusAsync("Third", 1, 10)).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Username, Is.EqualTo("ThirdUser"));
        }

        // ── FindArtistByUserIdAsync ──

        [Test]
        public async Task FindArtistByUserIdAsync_ReturnsArtistWhenFound()
        {
            var artistId = Guid.NewGuid();
            await artistRepo.AddAsync(new Artist { Id = artistId, UserId = testUser1.Id, IsAccepted = UploadStatus.Approved });

            var result = await artistService.FindArtistByUserIdAsync(testUser1.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Id, Is.EqualTo(artistId));
            Assert.That(result.IsAccepted, Is.EqualTo(UploadStatus.Approved));
        }

        [Test]
        public async Task FindArtistByUserIdAsync_ReturnsNullWhenNotFound()
        {
            var result = await artistService.FindArtistByUserIdAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        // ── AcceptArtistAsync ──

        [Test]
        public async Task AcceptArtistAsync_SetsStatusToApproved()
        {
            var artistId = Guid.NewGuid();
            await artistRepo.AddAsync(new Artist { Id = artistId, UserId = testUser1.Id, IsAccepted = UploadStatus.Pending });

            await artistService.AcceptArtistAsync(artistId);

            var artist = await artistRepo.GetByIdAsync(artistId);
            Assert.That(artist!.IsAccepted, Is.EqualTo(UploadStatus.Approved));
        }

        [Test]
        public void AcceptArtistAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await artistService.AcceptArtistAsync(Guid.NewGuid()));
        }

        // ── RejectArtistAsync ──

        [Test]
        public async Task RejectArtistAsync_SetsStatusToRejected()
        {
            var artistId = Guid.NewGuid();
            await artistRepo.AddAsync(new Artist { Id = artistId, UserId = testUser1.Id, IsAccepted = UploadStatus.Pending });

            await artistService.RejectArtistAsync(artistId);

            var artist = await artistRepo.GetByIdAsync(artistId);
            Assert.That(artist!.IsAccepted, Is.EqualTo(UploadStatus.Rejected));
        }

        [Test]
        public void RejectArtistAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await artistService.RejectArtistAsync(Guid.NewGuid()));
        }
    }
}
