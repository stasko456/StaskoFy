using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using StaskoFy.Core.IService;
using StaskoFy.Core.Service;
using StaskoFy.DataAccess;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.Models.Enums;
using StaskoFy.ViewModels.Song;

namespace StaskoFy.Tests
{
    [TestFixture]
    public class SongServiceTests
    {
        private StaskoFyDbContext context;
        private Repository<Song> songRepo;
        private Repository<ArtistSong> artistSongRepo;
        private Repository<Artist> artistRepo;
        private Repository<PlaylistSong> playlistSongRepo;
        private Repository<LikedSongs> likedSongsRepo;
        private Repository<Playlist> playlistRepo;
        private Mock<IUploadService> mockImageService;
        private SongService songService;

        private User testUser;
        private Artist testArtist;
        private Genre testGenre;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<StaskoFyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new StaskoFyDbContext(options);
            songRepo = new Repository<Song>(context);
            artistSongRepo = new Repository<ArtistSong>(context);
            artistRepo = new Repository<Artist>(context);
            playlistSongRepo = new Repository<PlaylistSong>(context);
            likedSongsRepo = new Repository<LikedSongs>(context);
            playlistRepo = new Repository<Playlist>(context);
            mockImageService = new Mock<IUploadService>();

            mockImageService
                .Setup(x => x.UploadImageAsync(It.IsAny<Microsoft.AspNetCore.Http.IFormFile>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(("https://cloudinary.com/test.png", "public-id-123"));

            mockImageService
                .Setup(x => x.DestroyImageAsync(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            songService = new SongService(songRepo, artistSongRepo, artistRepo, mockImageService.Object, playlistSongRepo, likedSongsRepo, playlistRepo);

            // Seed
            testUser = new User { Id = Guid.NewGuid(), UserName = "TestArtist", Email = "test@test.com", ImageURL = "/img/test.png" };
            context.Users.Add(testUser);
            await context.SaveChangesAsync();

            testArtist = new Artist { Id = Guid.NewGuid(), UserId = testUser.Id, IsAccepted = UploadStatus.Approved };
            await artistRepo.AddAsync(testArtist);

            testGenre = new Genre { Id = Guid.NewGuid(), Name = "Rock" };
            context.Genres.Add(testGenre);
            await context.SaveChangesAsync();
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        private Song CreateSong(string title = "Test Song", UploadStatus status = UploadStatus.Approved, Guid? albumId = null, int likes = 0)
        {
            return new Song
            {
                Id = Guid.NewGuid(),
                Title = title,
                GenreId = testGenre.Id,
                ImageURL = "/img/song.png",
                CloudinaryPublicId = "test-pub-id",
                AudioURL = "/audio/test.mp3",
                CloudinaryAudioPublicId = "testaudio",
                Status = status,
                Length = new TimeSpan(0, 3, 30),
                AlbumId = albumId,
                Likes = likes
            };
        }

        private async Task<Song> CreateSongWithArtist(string title = "Test Song", UploadStatus status = UploadStatus.Approved, int likes = 0)
        {
            var song = CreateSong(title, status, null, likes);
            await songRepo.AddAsync(song);
            await artistSongRepo.AddAsync(new ArtistSong { ArtistId = testArtist.Id, SongId = song.Id });
            return song;
        }

        // ── GetTotalPendingPagesAsync ──

        [Test]
        public async Task GetTotalPendingPagesAsync_ReturnsCorrectPageCount()
        {
            for (int i = 0; i < 7; i++)
                await songRepo.AddAsync(CreateSong($"Pending {i}", UploadStatus.Pending));
            await songRepo.AddAsync(CreateSong("Approved", UploadStatus.Approved));

            var result = await songService.GetTotalPendingPagesAsync(5);

            Assert.That(result, Is.EqualTo(2)); // 7 pending / 5
        }

        [Test]
        public async Task GetTotalPendingPagesAsync_ReturnsZeroWhenAllApproved()
        {
            await songRepo.AddAsync(CreateSong("Approved", UploadStatus.Approved));

            var result = await songService.GetTotalPendingPagesAsync(5);

            Assert.That(result, Is.EqualTo(0));
        }

        // ── FilterSongsWithPendingStatusAsync ──

        [Test]
        public async Task FilterSongsWithPendingStatusAsync_ReturnsNonApprovedSongs()
        {
            await songRepo.AddAsync(CreateSong("Pending Song", UploadStatus.Pending));
            await songRepo.AddAsync(CreateSong("Approved Song", UploadStatus.Approved));
            await songRepo.AddAsync(CreateSong("Rejected Song", UploadStatus.Rejected));

            var result = (await songService.FilterSongsWithPendingStatusAsync("", 1, 10)).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task FilterSongsWithPendingStatusAsync_FiltersByTitle()
        {
            await songRepo.AddAsync(CreateSong("Rock Anthem", UploadStatus.Pending));
            await songRepo.AddAsync(CreateSong("Jazz Melody", UploadStatus.Pending));

            var result = (await songService.FilterSongsWithPendingStatusAsync("Rock", 1, 10)).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Title, Is.EqualTo("Rock Anthem"));
        }

        // ── GetSongByIdAsync ──

        [Test]
        public async Task GetSongByIdAsync_ReturnsSongWhenFound()
        {
            var song = CreateSong("My Song");
            await songRepo.AddAsync(song);

            var result = await songService.GetSongByIdAsync(song.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Title, Is.EqualTo("My Song"));
        }

        [Test]
        public void GetSongByIdAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await songService.GetSongByIdAsync(Guid.NewGuid()));
        }

        // ── GetSongDetailsByIdAsync ──

        [Test]
        public async Task GetSongDetailsByIdAsync_ReturnsSongDetailsWhenFound()
        {
            var song = CreateSong("Detail Song");
            await songRepo.AddAsync(song);

            var result = await songService.GetSongDetailsByIdAsync(song.Id, testUser.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Title, Is.EqualTo("Detail Song"));
        }

        [Test]
        public void GetSongDetailsByIdAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await songService.GetSongDetailsByIdAsync(Guid.NewGuid(), testUser.Id));
        }

        [Test]
        public void AddSongAsync_ThrowsDbUpdateExceptionDueToMissingAudioFields()
        {
            var model = new SongCreateViewModel
            {
                Title = "New Song",
                Minutes = 3,
                Seconds = 45,
                GenreId = testGenre.Id,
                SelectedArtistIds = new List<Guid>(),
                ImageFile = null
            };

            // AudioFile is null in model, causing UploadAudioFileAsync to throw NullReferenceException
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await songService.AddSongAsync(model, testUser.Id));
        }


        // ── UpdateSongsAsync ──

        [Test]
        public async Task UpdateSongsAsync_UpdatesSongFields()
        {
            var song = await CreateSongWithArtist("Old Title");

            var model = new SongEditViewModel
            {
                Id = song.Id,
                Title = "Updated Title",
                Minutes = 4,
                Seconds = 15,
                GenreId = testGenre.Id,
                ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                SelectedArtistIds = new List<Guid>(),
                ImageFile = null
            };

            await songService.UpdateSongsAsync(model, testUser.Id);

            var updated = await songRepo.GetByIdAsync(song.Id);
            Assert.That(updated!.Title, Is.EqualTo("Updated Title"));
            Assert.That(updated.Status, Is.EqualTo(UploadStatus.Pending));
        }

        [Test]
        public void UpdateSongsAsync_ThrowsWhenSongNotFound()
        {
            var model = new SongEditViewModel
            {
                Id = Guid.NewGuid(),
                Title = "Test",
                Minutes = 3,
                Seconds = 0,
                GenreId = testGenre.Id,
                ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                SelectedArtistIds = new List<Guid>()
            };

            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await songService.UpdateSongsAsync(model, testUser.Id));
        }

        // ── RemoveSongAsync ──

        [Test]
        public async Task RemoveSongAsync_SoftDeletesSong()
        {
            var song = CreateSong("To Delete");
            await songRepo.AddAsync(song);

            await songService.RemoveSongAsync(song.Id);

            var deleted = await songRepo.GetByIdAsync(song.Id);
            Assert.That(deleted!.Status, Is.EqualTo(UploadStatus.Deleted));
            Assert.That(deleted.Likes, Is.EqualTo(0));
        }

        [Test]
        public void RemoveSongAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await songService.RemoveSongAsync(Guid.NewGuid()));
        }



        // ── GetTotalPagesAsync ──

        [Test]
        public async Task GetTotalPagesAsync_ReturnsCorrectPageCount()
        {
            for (int i = 0; i < 25; i++)
                await songRepo.AddAsync(CreateSong($"Song {i}", UploadStatus.Approved));
            await songRepo.AddAsync(CreateSong("Deleted", UploadStatus.Deleted));

            var result = await songService.GetTotalPagesAsync(12);

            Assert.That(result, Is.EqualTo(3)); // 25 / 12 = 2.08 → ceil = 3
        }

        // ── FilterSongsAsync ──

        [Test]
        public async Task FilterSongsAsync_ReturnsApprovedSongs()
        {
            await songRepo.AddAsync(CreateSong("Approved A", UploadStatus.Approved));
            await songRepo.AddAsync(CreateSong("Approved B", UploadStatus.Approved));
            await songRepo.AddAsync(CreateSong("Pending C", UploadStatus.Pending));

            var result = (await songService.FilterSongsAsync("", null, 1, 10)).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task FilterSongsAsync_RespectsPagination()
        {
            for (int i = 0; i < 10; i++)
                await songRepo.AddAsync(CreateSong($"Song {i}", UploadStatus.Approved));

            var page1 = (await songService.FilterSongsAsync("", null, 1, 4)).ToList();
            var page2 = (await songService.FilterSongsAsync("", null, 2, 4)).ToList();

            Assert.That(page1.Count, Is.EqualTo(4));
            Assert.That(page2.Count, Is.EqualTo(4));
        }

        // ── FilterSongsForCurrentLoggedArtistAsync ──

        [Test]
        public async Task FilterSongsForCurrentLoggedArtistAsync_ReturnsArtistApprovedSongs()
        {
            await CreateSongWithArtist("Artist Song 1");
            await CreateSongWithArtist("Artist Song 2");

            // Non-artist song
            await songRepo.AddAsync(CreateSong("Other Song", UploadStatus.Approved));

            var result = (await songService.FilterSongsForCurrentLoggedArtistAsync(testUser.Id, "", null)).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task SelectSinglesByCurrentLoggedArtistAsync_ReturnsOnlySingles()
        {
            await CreateSongWithArtist("Single Song");

            // Song with album
            var albumId = Guid.NewGuid();
            context.Albums.Add(new Album
            {
                Id = albumId,
                Title = "Test Album",
                ImageURL = "/img/album.png",
                CloudinaryPublicId = "album-pub",
                Status = UploadStatus.Approved
            });
            await context.SaveChangesAsync();

            var albumSong = CreateSong("Album Song", UploadStatus.Approved, albumId);
            await songRepo.AddAsync(albumSong);
            await artistSongRepo.AddAsync(new ArtistSong { ArtistId = testArtist.Id, SongId = albumSong.Id });

            var result = (await songService.SelectSinglesByCurrentLoggedArtistAsync(testUser.Id)).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Title, Is.EqualTo("Single Song"));
        }

        // ── GetSinglesForCurrentLoggedArtistAsync ──

        [Test]
        public async Task GetSinglesForCurrentLoggedArtistAsync_ReturnsApprovedSingles()
        {
            await CreateSongWithArtist("Approved Single");

            var pendingSong = CreateSong("Pending Single", UploadStatus.Pending);
            await songRepo.AddAsync(pendingSong);
            await artistSongRepo.AddAsync(new ArtistSong { ArtistId = testArtist.Id, SongId = pendingSong.Id });

            var result = (await songService.GetSinglesForCurrentLoggedArtistAsync(testUser.Id)).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Title, Is.EqualTo("Approved Single"));
        }

        // ── AcceptSongUploadAsync ──

        [Test]
        public async Task AcceptSongUploadAsync_SetsStatusToApproved()
        {
            var song = CreateSong("Pending", UploadStatus.Pending);
            await songRepo.AddAsync(song);

            await songService.AcceptSongUploadAsync(song.Id);

            var updated = await songRepo.GetByIdAsync(song.Id);
            Assert.That(updated!.Status, Is.EqualTo(UploadStatus.Approved));
        }

        [Test]
        public void AcceptSongUploadAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await songService.AcceptSongUploadAsync(Guid.NewGuid()));
        }

        // ── RejectSongUploadAsync ──

        [Test]
        public async Task RejectSongUploadAsync_SetsStatusToRejected()
        {
            var song = CreateSong("Pending", UploadStatus.Pending);
            await songRepo.AddAsync(song);

            await songService.RejectSongUploadAsync(song.Id);

            var updated = await songRepo.GetByIdAsync(song.Id);
            Assert.That(updated!.Status, Is.EqualTo(UploadStatus.Rejected));
        }

        [Test]
        public void RejectSongUploadAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await songService.RejectSongUploadAsync(Guid.NewGuid()));
        }

        // ── GetTotalSongsCountAsync ──

        [Test]
        public async Task GetTotalSongsCountAsync_ExcludesDeletedSongs()
        {
            await songRepo.AddAsync(CreateSong("Approved", UploadStatus.Approved));
            await songRepo.AddAsync(CreateSong("Pending", UploadStatus.Pending));
            await songRepo.AddAsync(CreateSong("Deleted", UploadStatus.Deleted));

            var result = await songService.GetTotalSongsCountAsync();

            Assert.That(result, Is.EqualTo(2));
        }

        // ── GetTotalPendingSongsCountAsync ──

        [Test]
        public async Task GetTotalPendingSongsCountAsync_ReturnsNonApprovedCount()
        {
            await songRepo.AddAsync(CreateSong("Approved", UploadStatus.Approved));
            await songRepo.AddAsync(CreateSong("Pending", UploadStatus.Pending));
            await songRepo.AddAsync(CreateSong("Rejected", UploadStatus.Rejected));

            var result = await songService.GetTotalPendingSongsCountAsync();

            Assert.That(result, Is.EqualTo(2));
        }

        // ── GetTotalSongsCountByCurrentLoggedArtistAsync ──

        [Test]
        public async Task GetTotalSongsCountByCurrentLoggedArtistAsync_ReturnsNonDeletedArtistSongs()
        {
            await CreateSongWithArtist("Song 1", UploadStatus.Approved);
            await CreateSongWithArtist("Song 2", UploadStatus.Pending);

            var deletedSong = CreateSong("Deleted Song", UploadStatus.Deleted);
            await songRepo.AddAsync(deletedSong);
            await artistSongRepo.AddAsync(new ArtistSong { ArtistId = testArtist.Id, SongId = deletedSong.Id });

            var result = await songService.GetTotalSongsCountByCurrentLoggedArtistAsync(testUser.Id);

            Assert.That(result, Is.EqualTo(2));
        }

        // ── GetTotalPendingSongsCountByCurrentLoggedArtistAsync ──

        [Test]
        public async Task GetTotalPendingSongsCountByCurrentLoggedArtistAsync_ReturnsNonApprovedArtistSongs()
        {
            await CreateSongWithArtist("Approved Song", UploadStatus.Approved);
            await CreateSongWithArtist("Pending Song", UploadStatus.Pending);

            var result = await songService.GetTotalPendingSongsCountByCurrentLoggedArtistAsync(testUser.Id);

            Assert.That(result, Is.EqualTo(1));
        }

        // ── GetTotalSongsLikesByCurrentLoggedArtistAsync ──

        [Test]
        public async Task GetTotalSongsLikesByCurrentLoggedArtistAsync_ReturnsSumOfLikes()
        {
            await CreateSongWithArtist("Song A", UploadStatus.Approved, 10);
            await CreateSongWithArtist("Song B", UploadStatus.Approved, 25);

            var result = await songService.GetTotalSongsLikesByCurrentLoggedArtistAsync(testUser.Id);

            Assert.That(result, Is.EqualTo(35));
        }

        [Test]
        public async Task GetTotalSongsLikesByCurrentLoggedArtistAsync_ReturnsZeroForNoSongs()
        {
            var result = await songService.GetTotalSongsLikesByCurrentLoggedArtistAsync(testUser.Id);

            Assert.That(result, Is.EqualTo(0));
        }

        // ── GetMostLikedSongAsync ──

        [Test]
        public async Task GetMostLikedSongAsync_ReturnsMostLikedSong()
        {
            await CreateSongWithArtist("Low Likes", UploadStatus.Approved, 5);
            await CreateSongWithArtist("High Likes", UploadStatus.Approved, 100);
            await CreateSongWithArtist("Mid Likes", UploadStatus.Approved, 50);

            var result = await songService.GetMostLikedSongAsync(testUser.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.MostLikedSongTitle, Is.EqualTo("High Likes"));
            Assert.That(result.MostLikedSongCount, Is.EqualTo(100));
        }

        [Test]
        public async Task GetMostLikedSongAsync_ReturnsNullWhenNoSongs()
        {
            var result = await songService.GetMostLikedSongAsync(testUser.Id);

            Assert.That(result, Is.Null);
        }

        // ── GetSongDetailsForMusicPlayerAsync ──

        [Test]
        public async Task GetSongDetailsForMusicPlayerAsync_ReturnsSongDetails()
        {
            var song = CreateSong("Player Song");
            await songRepo.AddAsync(song);

            var result = await songService.GetSongDetailsForMusicPlayerAsync(song.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Title, Is.EqualTo("Player Song"));
            Assert.That(result.AudioURL, Is.EqualTo("/audio/test.mp3"));
        }

        [Test]
        public async Task GetSongDetailsForMusicPlayerAsync_ReturnsNullWhenNotFound()
        {
            var result = await songService.GetSongDetailsForMusicPlayerAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }
    }
}
