using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StaskoFy.Core.Service;
using StaskoFy.DataAccess;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.Models.Enums;
using StaskoFy.ViewModels.LikedSongs;

namespace StaskoFy.Tests
{
    [TestFixture]
    public class LikedSongsServiceTests
    {
        private StaskoFyDbContext context;
        private Repository<LikedSongs> likedSongsRepo;
        private Repository<Song> songRepo;
        private LikedSongsService likedSongsService;

        private User testUser;
        private Genre testGenre;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<StaskoFyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new StaskoFyDbContext(options);
            likedSongsRepo = new Repository<LikedSongs>(context);
            songRepo = new Repository<Song>(context);
            likedSongsService = new LikedSongsService(likedSongsRepo, songRepo);

            // Seed user and genre
            testUser = new User { Id = Guid.NewGuid(), UserName = "TestUser", Email = "test@test.com", ImageURL = "/img/test.png", CloudinaryPublicId = "test-pub" };
            testGenre = new Genre { Id = Guid.NewGuid(), Name = "Rock" };

            context.Users.Add(testUser);
            context.Genres.Add(testGenre);
            await context.SaveChangesAsync();
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        private Song CreateSong(string title = "Test Song", UploadStatus status = UploadStatus.Approved, int likes = 0)
        {
            return new Song
            {
                Id = Guid.NewGuid(),
                Title = title,
                GenreId = testGenre.Id,
                ImageURL = "/img/song.png",
                CloudinaryPublicId = "test",
                AudioURL = "/audio/test.mp3",
                CloudinaryAudioPublicId = "testaudio",
                Status = status,
                Likes = likes,
                Length = new TimeSpan(0, 3, 30)
            };
        }

        // ── GetTotalPagesAsync ──

        [Test]
        public async Task GetTotalPagesAsync_ReturnsCorrectPageCount()
        {
            for (int i = 0; i < 7; i++)
            {
                var song = CreateSong($"Song {i}");
                await songRepo.AddAsync(song);
                await likedSongsRepo.AddAsync(new LikedSongs
                {
                    Id = Guid.NewGuid(),
                    UserId = testUser.Id,
                    SongId = song.Id,
                    DateAdded = DateOnly.FromDateTime(DateTime.Now)
                });
            }

            var result = await likedSongsService.GetTotalPagesAsync(testUser.Id, 5);

            Assert.That(result, Is.EqualTo(2)); // 7 / 5 = 1.4 → ceil = 2
        }

        [Test]
        public async Task GetTotalPagesAsync_ExcludesNonApprovedSongs()
        {
            var approvedSong = CreateSong("Approved", UploadStatus.Approved);
            var pendingSong = CreateSong("Pending", UploadStatus.Pending);
            await songRepo.AddAsync(approvedSong);
            await songRepo.AddAsync(pendingSong);

            await likedSongsRepo.AddAsync(new LikedSongs { Id = Guid.NewGuid(), UserId = testUser.Id, SongId = approvedSong.Id, DateAdded = DateOnly.FromDateTime(DateTime.Now) });
            await likedSongsRepo.AddAsync(new LikedSongs { Id = Guid.NewGuid(), UserId = testUser.Id, SongId = pendingSong.Id, DateAdded = DateOnly.FromDateTime(DateTime.Now) });

            var result = await likedSongsService.GetTotalPagesAsync(testUser.Id, 5);

            Assert.That(result, Is.EqualTo(1)); // Only 1 approved
        }

        // ── GetLikedSongsFromCurrentLoggedUserAsync ──

        [Test]
        public async Task GetLikedSongsFromCurrentLoggedUserAsync_ReturnsUserLikedSongs()
        {
            var song1 = CreateSong("Song One");
            var song2 = CreateSong("Song Two");
            await songRepo.AddAsync(song1);
            await songRepo.AddAsync(song2);

            await likedSongsRepo.AddAsync(new LikedSongs { Id = Guid.NewGuid(), UserId = testUser.Id, SongId = song1.Id, DateAdded = DateOnly.FromDateTime(DateTime.Now) });
            await likedSongsRepo.AddAsync(new LikedSongs { Id = Guid.NewGuid(), UserId = testUser.Id, SongId = song2.Id, DateAdded = DateOnly.FromDateTime(DateTime.Now) });

            var result = (await likedSongsService.GetLikedSongsFromCurrentLoggedUserAsync(testUser.Id, "", 1, 10)).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetLikedSongsFromCurrentLoggedUserAsync_FiltersByName()
        {
            var song1 = CreateSong("Bohemian Rhapsody");
            var song2 = CreateSong("Stairway to Heaven");
            await songRepo.AddAsync(song1);
            await songRepo.AddAsync(song2);

            await likedSongsRepo.AddAsync(new LikedSongs { Id = Guid.NewGuid(), UserId = testUser.Id, SongId = song1.Id, DateAdded = DateOnly.FromDateTime(DateTime.Now) });
            await likedSongsRepo.AddAsync(new LikedSongs { Id = Guid.NewGuid(), UserId = testUser.Id, SongId = song2.Id, DateAdded = DateOnly.FromDateTime(DateTime.Now) });

            var result = (await likedSongsService.GetLikedSongsFromCurrentLoggedUserAsync(testUser.Id, "Bohemian", 1, 10)).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Title, Is.EqualTo("Bohemian Rhapsody"));
        }

        [Test]
        public async Task GetLikedSongsFromCurrentLoggedUserAsync_RespectsPagination()
        {
            for (int i = 0; i < 8; i++)
            {
                var song = CreateSong($"Song {i}");
                await songRepo.AddAsync(song);
                await likedSongsRepo.AddAsync(new LikedSongs
                {
                    Id = Guid.NewGuid(),
                    UserId = testUser.Id,
                    SongId = song.Id,
                    DateAdded = DateOnly.FromDateTime(DateTime.Now)
                });
            }

            var page1 = (await likedSongsService.GetLikedSongsFromCurrentLoggedUserAsync(testUser.Id, "", 1, 5)).ToList();
            var page2 = (await likedSongsService.GetLikedSongsFromCurrentLoggedUserAsync(testUser.Id, "", 2, 5)).ToList();

            Assert.That(page1.Count, Is.EqualTo(5));
            Assert.That(page2.Count, Is.EqualTo(3));
        }

        // ── GetLikedSongByUserAndSongAsync ──

        [Test]
        public async Task GetLikedSongByUserAndSongAsync_ReturnsWhenFound()
        {
            var song = CreateSong();
            await songRepo.AddAsync(song);

            var likedSong = new LikedSongs { Id = Guid.NewGuid(), UserId = testUser.Id, SongId = song.Id, DateAdded = DateOnly.FromDateTime(DateTime.Now) };
            await likedSongsRepo.AddAsync(likedSong);

            var result = await likedSongsService.GetLikedSongByUserAndSongAsync(testUser.Id, song.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.SongId, Is.EqualTo(song.Id));
        }

        [Test]
        public async Task GetLikedSongByUserAndSongAsync_ReturnsNullWhenNotFound()
        {
            var result = await likedSongsService.GetLikedSongByUserAndSongAsync(testUser.Id, Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        // ── AddLikedSongAsync ──

        [Test]
        public async Task AddLikedSongAsync_AddsLikedSongAndIncreasesLikes()
        {
            var song = CreateSong("My Song", UploadStatus.Approved, 0);
            await songRepo.AddAsync(song);

            var model = new LikedSongsCreateViewModel { SongId = song.Id };
            await likedSongsService.AddLikedSongAsync(model, testUser.Id);

            var likedSongs = await likedSongsRepo.GetAllAttached().ToListAsync();
            Assert.That(likedSongs.Count, Is.EqualTo(1));
            Assert.That(likedSongs[0].UserId, Is.EqualTo(testUser.Id));

            var updatedSong = await songRepo.GetByIdAsync(song.Id);
            Assert.That(updatedSong!.Likes, Is.EqualTo(1));
        }

        [Test]
        public void AddLikedSongAsync_ThrowsWhenSongNotFound()
        {
            var model = new LikedSongsCreateViewModel { SongId = Guid.NewGuid() };

            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await likedSongsService.AddLikedSongAsync(model, testUser.Id));
        }

        // ── RemoveLikedSongAsync ──

        [Test]
        public async Task RemoveLikedSongAsync_RemovesLikeAndDecreasesLikes()
        {
            var song = CreateSong("My Song", UploadStatus.Approved, 3);
            await songRepo.AddAsync(song);

            await likedSongsRepo.AddAsync(new LikedSongs { Id = Guid.NewGuid(), UserId = testUser.Id, SongId = song.Id, DateAdded = DateOnly.FromDateTime(DateTime.Now) });

            await likedSongsService.RemoveLikedSongAsync(testUser.Id, song.Id);

            var likedSongs = await likedSongsRepo.GetAllAttached().ToListAsync();
            Assert.That(likedSongs.Count, Is.EqualTo(0));

            var updatedSong = await songRepo.GetByIdAsync(song.Id);
            Assert.That(updatedSong!.Likes, Is.EqualTo(2));
        }

        [Test]
        public void RemoveLikedSongAsync_ThrowsWhenLikedSongNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await likedSongsService.RemoveLikedSongAsync(testUser.Id, Guid.NewGuid()));
        }

        [Test]
        public async Task RemoveLikedSongAsync_DoesNotDecreaseBelowZero()
        {
            var song = CreateSong("My Song", UploadStatus.Approved, 0);
            await songRepo.AddAsync(song);

            await likedSongsRepo.AddAsync(new LikedSongs { Id = Guid.NewGuid(), UserId = testUser.Id, SongId = song.Id, DateAdded = DateOnly.FromDateTime(DateTime.Now) });

            await likedSongsService.RemoveLikedSongAsync(testUser.Id, song.Id);

            var updatedSong = await songRepo.GetByIdAsync(song.Id);
            Assert.That(updatedSong!.Likes, Is.EqualTo(0));
        }

        // ── GetTotalLikedSongsByCurrentLoggedUserAsync ──

        [Test]
        public async Task GetTotalLikedSongsByCurrentLoggedUserAsync_ReturnsCorrectCount()
        {
            for (int i = 0; i < 5; i++)
            {
                var song = CreateSong($"Song {i}");
                await songRepo.AddAsync(song);
                await likedSongsRepo.AddAsync(new LikedSongs
                {
                    Id = Guid.NewGuid(),
                    UserId = testUser.Id,
                    SongId = song.Id,
                    DateAdded = DateOnly.FromDateTime(DateTime.Now)
                });
            }

            var result = await likedSongsService.GetTotalLikedSongsByCurrentLoggedUserAsync(testUser.Id);

            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public async Task GetTotalLikedSongsByCurrentLoggedUserAsync_ReturnsZeroForNoLikes()
        {
            var result = await likedSongsService.GetTotalLikedSongsByCurrentLoggedUserAsync(testUser.Id);

            Assert.That(result, Is.EqualTo(0));
        }

        // ── GetLengthOfLikedSongsByCurrentLoggedUserAsync ──

        [Test]
        public async Task GetLengthOfLikedSongsByCurrentLoggedUserAsync_ReturnsTotalDuration()
        {
            var song1 = CreateSong("Song 1");
            song1.Length = new TimeSpan(0, 3, 30);
            var song2 = CreateSong("Song 2");
            song2.Length = new TimeSpan(0, 4, 15);
            await songRepo.AddAsync(song1);
            await songRepo.AddAsync(song2);

            await likedSongsRepo.AddAsync(new LikedSongs { Id = Guid.NewGuid(), UserId = testUser.Id, SongId = song1.Id, DateAdded = DateOnly.FromDateTime(DateTime.Now) });
            await likedSongsRepo.AddAsync(new LikedSongs { Id = Guid.NewGuid(), UserId = testUser.Id, SongId = song2.Id, DateAdded = DateOnly.FromDateTime(DateTime.Now) });

            var result = await likedSongsService.GetLengthOfLikedSongsByCurrentLoggedUserAsync(testUser.Id);

            Assert.That(result, Is.EqualTo(new TimeSpan(0, 7, 45)));
        }

        [Test]
        public async Task GetLengthOfLikedSongsByCurrentLoggedUserAsync_ReturnsZeroWhenNoLikes()
        {
            var result = await likedSongsService.GetLengthOfLikedSongsByCurrentLoggedUserAsync(testUser.Id);

            Assert.That(result, Is.EqualTo(TimeSpan.Zero));
        }
    }
}
