using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using StaskoFy.Core.IService;
using StaskoFy.Core.Service;
using StaskoFy.DataAccess;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.Models.Enums;
using StaskoFy.ViewModels.Album;

namespace StaskoFy.Tests
{
    [TestFixture]
    public class AlbumServiceTests
    {
        private StaskoFyDbContext context;
        private Repository<Album> albumRepo;
        private Repository<Artist> artistRepo;
        private Repository<ArtistAlbum> artistAlbumRepo;
        private Repository<Song> songRepo;
        private Mock<IUploadService> mockImageService;
        private AlbumService albumService;

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
            albumRepo = new Repository<Album>(context);
            artistRepo = new Repository<Artist>(context);
            artistAlbumRepo = new Repository<ArtistAlbum>(context);
            songRepo = new Repository<Song>(context);
            mockImageService = new Mock<IUploadService>();

            mockImageService
                .Setup(x => x.UploadImageAsync(It.IsAny<Microsoft.AspNetCore.Http.IFormFile>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(("https://cloudinary.com/test.png", "public-id-123"));

            mockImageService
                .Setup(x => x.DestroyImageAsync(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            albumService = new AlbumService(albumRepo, artistRepo, artistAlbumRepo, songRepo, mockImageService.Object);

            // Seed
            testUser = new User { Id = Guid.NewGuid(), UserName = "TestArtist", Email = "test@test.com", ImageURL = "/img/test.png", CloudinaryPublicId = "test-pub" };
            context.Users.Add(testUser);
            await context.SaveChangesAsync();

            testArtist = new Artist { Id = Guid.NewGuid(), UserId = testUser.Id, IsAccepted = UploadStatus.Approved };
            await artistRepo.AddAsync(testArtist);

            testGenre = new Genre { Id = Guid.NewGuid(), Name = "Rock"};
            context.Genres.Add(testGenre);
            await context.SaveChangesAsync();
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        private Album CreateAlbum(string title = "Test Album", UploadStatus status = UploadStatus.Approved)
        {
            return new Album
            {
                Id = Guid.NewGuid(),
                Title = title,
                ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                ImageURL = "/img/album.png",
                CloudinaryPublicId = "album-pub-id",
                Status = status,
                Length = TimeSpan.Zero
            };
        }

        private Song CreateSong(string title = "Test Song", UploadStatus status = UploadStatus.Approved)
        {
            return new Song
            {
                Id = Guid.NewGuid(),
                Title = title,
                GenreId = testGenre.Id,
                ImageURL = "/img/song.png",
                CloudinaryPublicId = "song-pub-id",
                AudioURL = "/audio/test.mp3",
                CloudinaryAudioPublicId = "audio-pub",
                Status = status,
                Length = new TimeSpan(0, 3, 30)
            };
        }

        private async Task<Album> CreateAlbumWithArtist(string title = "Test Album", UploadStatus status = UploadStatus.Approved)
        {
            var album = CreateAlbum(title, status);
            await albumRepo.AddAsync(album);
            await artistAlbumRepo.AddAsync(new ArtistAlbum { ArtistId = testArtist.Id, AlbumId = album.Id });
            return album;
        }

        // ── GetTotalPendingPagesAsync ──

        [Test]
        public async Task GetTotalPendingPagesAsync_ReturnsCorrectPageCount()
        {
            for (int i = 0; i < 6; i++)
                await songRepo.AddAsync(CreateSong($"Pending {i}", UploadStatus.Pending));
            await songRepo.AddAsync(CreateSong("Approved", UploadStatus.Approved));

            var result = await albumService.GetTotalPendingPagesAsync(4);

            Assert.That(result, Is.EqualTo(2)); // 6 non-approved / 4 = 1.5 → ceil = 2
        }

        // ── FilterAlbumsWithPendingStatusAsync ──

        [Test]
        public async Task FilterAlbumsWithPendingStatusAsync_ReturnsNonApprovedAlbums()
        {
            await albumRepo.AddAsync(CreateAlbum("Pending Album", UploadStatus.Pending));
            await albumRepo.AddAsync(CreateAlbum("Approved Album", UploadStatus.Approved));
            await albumRepo.AddAsync(CreateAlbum("Rejected Album", UploadStatus.Rejected));

            var result = (await albumService.FilterAlbumsWithPendingStatusAsync("", 1, 10)).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task FilterAlbumsWithPendingStatusAsync_FiltersByTitle()
        {
            await albumRepo.AddAsync(CreateAlbum("Rock Album", UploadStatus.Pending));
            await albumRepo.AddAsync(CreateAlbum("Jazz Album", UploadStatus.Pending));

            var result = (await albumService.FilterAlbumsWithPendingStatusAsync("Rock", 1, 10)).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
        }

        // ── GetSpecificArtistAlbumsAsync ──

        [Test]
        public async Task GetSpecificArtistAlbumsAsync_ReturnsArtistApprovedAlbums()
        {
            await CreateAlbumWithArtist("Album 1", UploadStatus.Approved);
            await CreateAlbumWithArtist("Album 2", UploadStatus.Approved);
            await CreateAlbumWithArtist("Pending Album", UploadStatus.Pending);

            var result = (await albumService.GetSpecificArtistAlbumsAsync(testUser.Id)).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetSpecificArtistAlbumsAsync_ReturnsEmptyForNonArtist()
        {
            var result = (await albumService.GetSpecificArtistAlbumsAsync(Guid.NewGuid())).ToList();

            Assert.That(result.Count, Is.EqualTo(0));
        }

        // ── GetAlbumByIdAsync ──

        [Test]
        public async Task GetAlbumByIdAsync_ReturnsAlbumWhenFound()
        {
            var album = CreateAlbum("Found Album");
            await albumRepo.AddAsync(album);

            var result = await albumService.GetAlbumByIdAsync(album.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Title, Is.EqualTo("Found Album"));
        }

        [Test]
        public void GetAlbumByIdAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await albumService.GetAlbumByIdAsync(Guid.NewGuid()));
        }

        // ── GetAlbumByIdWithSongsAsync ──

        [Test]
        public async Task GetAlbumByIdWithSongsAsync_ReturnsAlbumWithSongs()
        {
            var album = CreateAlbum("Album With Songs");
            await albumRepo.AddAsync(album);

            var song = CreateSong("Album Song");
            song.AlbumId = album.Id;
            await songRepo.AddAsync(song);

            var result = await albumService.GetAlbumByIdWithSongsAsync(album.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Title, Is.EqualTo("Album With Songs"));
            Assert.That(result.Songs.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetAlbumByIdWithSongsAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await albumService.GetAlbumByIdWithSongsAsync(Guid.NewGuid()));
        }

        // ── AddAlbumAsync ──

        [Test]
        public async Task AddAlbumAsync_CreatesAlbumWithPendingStatus()
        {
            var model = new AlbumCreateViewModel
            {
                Title = "New Album",
                SelectedArtistIds = new List<Guid>(),
                SelectedSongIds = new List<Guid>(),
                ImageFile = null
            };

            await albumService.AddAlbumAsync(model, testUser.Id);

            var albums = await albumRepo.GetAllAttached().ToListAsync();
            Assert.That(albums.Count, Is.EqualTo(1));
            Assert.That(albums[0].Title, Is.EqualTo("New Album"));
            Assert.That(albums[0].Status, Is.EqualTo(UploadStatus.Pending));
        }

        [Test]
        public async Task AddAlbumAsync_CalculatesAlbumLength()
        {
            var song1 = CreateSong("Song 1");
            song1.Length = new TimeSpan(0, 3, 0);
            var song2 = CreateSong("Song 2");
            song2.Length = new TimeSpan(0, 4, 30);
            await songRepo.AddAsync(song1);
            await songRepo.AddAsync(song2);

            var model = new AlbumCreateViewModel
            {
                Title = "Length Album",
                SelectedArtistIds = new List<Guid>(),
                SelectedSongIds = new List<Guid> { song1.Id, song2.Id },
                ImageFile = null
            };

            await albumService.AddAlbumAsync(model, testUser.Id);

            var album = await albumRepo.GetAllAttached().FirstAsync();
            Assert.That(album.Length, Is.EqualTo(new TimeSpan(0, 7, 30)));
        }

        // ── UpdateAlbumAsync ──

        [Test]
        public async Task UpdateAlbumAsync_UpdatesAlbumTitle()
        {
            var album = await CreateAlbumWithArtist("Old Title");

            var model = new AlbumEditViewModel
            {
                Id = album.Id,
                Title = "New Title",
                ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                SelectedArtistIds = new List<Guid>(),
                SelectedSongIds = new List<Guid>(),
                ImageFile = null
            };

            await albumService.UpdateAlbumAsync(model, testUser.Id);

            var updated = await albumRepo.GetByIdAsync(album.Id);
            Assert.That(updated!.Title, Is.EqualTo("New Title"));
            Assert.That(updated.Status, Is.EqualTo(UploadStatus.Pending));
        }

        [Test]
        public void UpdateAlbumAsync_ThrowsWhenNotFound()
        {
            var model = new AlbumEditViewModel
            {
                Id = Guid.NewGuid(),
                Title = "Test",
                ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                SelectedArtistIds = new List<Guid>(),
                SelectedSongIds = new List<Guid>()
            };

            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await albumService.UpdateAlbumAsync(model, testUser.Id));
        }

        // ── RemoveAlbumAsync ──

        [Test]
        public async Task RemoveAlbumAsync_SoftDeletesAlbum()
        {
            var album = CreateAlbum("To Delete");
            await albumRepo.AddAsync(album);

            await albumService.RemoveAlbumAsync(album.Id);

            var deleted = await albumRepo.GetByIdAsync(album.Id);
            Assert.That(deleted!.Status, Is.EqualTo(UploadStatus.Deleted));
            Assert.That(deleted.ImageURL, Is.EqualTo("/img/album.png"));
        }

        [Test]
        public async Task RemoveAlbumAsync_SoftDeletesSongsInAlbum()
        {
            var album = CreateAlbum("Album With Songs");
            await albumRepo.AddAsync(album);

            var song = CreateSong("Album Song");
            song.AlbumId = album.Id;
            await songRepo.AddAsync(song);

            await albumService.RemoveAlbumAsync(album.Id);

            var deletedSong = await songRepo.GetByIdAsync(song.Id);
            Assert.That(deletedSong!.Status, Is.EqualTo(UploadStatus.Deleted));
            Assert.That(deletedSong.Likes, Is.EqualTo(0));
        }

        [Test]
        public void RemoveAlbumAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await albumService.RemoveAlbumAsync(Guid.NewGuid()));
        }

        // ── GetTotalPagesAsync ──

        [Test]
        public async Task GetTotalPagesAsync_ReturnsCorrectPageCount()
        {
            for (int i = 0; i < 9; i++)
                await albumRepo.AddAsync(CreateAlbum($"Album {i}", UploadStatus.Approved));
            await albumRepo.AddAsync(CreateAlbum("Deleted", UploadStatus.Deleted));

            var result = await albumService.GetTotalPagesAsync(4);

            Assert.That(result, Is.EqualTo(3)); // 9 / 4 = 2.25 → ceil = 3
        }

        // ── FilterAlbumsAsync ──

        [Test]
        public async Task FilterAlbumsAsync_ReturnsApprovedAlbums()
        {
            await albumRepo.AddAsync(CreateAlbum("Approved A", UploadStatus.Approved));
            await albumRepo.AddAsync(CreateAlbum("Approved B", UploadStatus.Approved));
            await albumRepo.AddAsync(CreateAlbum("Pending C", UploadStatus.Pending));

            var result = (await albumService.FilterAlbumsAsync("", null, 1, 10)).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task FilterAlbumsAsync_RespectsPagination()
        {
            for (int i = 0; i < 8; i++)
                await albumRepo.AddAsync(CreateAlbum($"Album {i}", UploadStatus.Approved));

            var page1 = (await albumService.FilterAlbumsAsync("", null, 1, 4)).ToList();
            var page2 = (await albumService.FilterAlbumsAsync("", null, 2, 4)).ToList();

            Assert.That(page1.Count, Is.EqualTo(4));
            Assert.That(page2.Count, Is.EqualTo(4));
        }

        // ── FilterAlbumsForCurrentLoggedArtistAsync ──

        [Test]
        public async Task FilterAlbumsForCurrentLoggedArtistAsync_ReturnsArtistApprovedAlbums()
        {
            await CreateAlbumWithArtist("Artist Album 1");
            await CreateAlbumWithArtist("Artist Album 2");

            // Other album
            await albumRepo.AddAsync(CreateAlbum("Other Album"));

            var result = (await albumService.FilterAlbumsForCurrentLoggedArtistAsync(testUser.Id, "", null)).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        // ── RemoveSongFromAlbumAsync ──

        [Test]
        public async Task RemoveSongFromAlbumAsync_RemovesSongFromAlbum()
        {
            var album = CreateAlbum("Test Album");
            album.Length = new TimeSpan(0, 7, 0);
            await albumRepo.AddAsync(album);

            var song = CreateSong("Album Song");
            song.AlbumId = album.Id;
            song.Length = new TimeSpan(0, 3, 30);
            await songRepo.AddAsync(song);

            await albumService.RemoveSongFromAlbumAsync(song.Id);

            var updatedSong = await songRepo.GetByIdAsync(song.Id);
            Assert.That(updatedSong!.AlbumId, Is.Null);
            Assert.That(updatedSong.ImageURL, Is.EqualTo("/images/defaults/default-song-cover-art.png"));
        }

        [Test]
        public void RemoveSongFromAlbumAsync_ThrowsWhenSongNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await albumService.RemoveSongFromAlbumAsync(Guid.NewGuid()));
        }

        // ── AddSongToAlbumAsync ──

        [Test]
        public async Task AddSongToAlbumAsync_AddsSongToAlbum()
        {
            var album = CreateAlbum("Target Album");
            await albumRepo.AddAsync(album);

            var song = CreateSong("Single Song");
            song.Length = new TimeSpan(0, 4, 0);
            await songRepo.AddAsync(song);

            await albumService.AddSongToAlbumAsync(song.Id, album.Id);

            var updatedSong = await songRepo.GetByIdAsync(song.Id);
            Assert.That(updatedSong!.AlbumId, Is.EqualTo(album.Id));
            Assert.That(updatedSong.ImageURL, Is.EqualTo(album.ImageURL));

            var updatedAlbum = await albumRepo.GetByIdAsync(album.Id);
            Assert.That(updatedAlbum!.Length, Is.EqualTo(new TimeSpan(0, 4, 0)));
        }

        [Test]
        public void AddSongToAlbumAsync_ThrowsWhenSongNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await albumService.AddSongToAlbumAsync(Guid.NewGuid(), Guid.NewGuid()));
        }

        [Test]
        public async Task AddSongToAlbumAsync_ThrowsWhenAlbumNotFound()
        {
            var song = CreateSong("Orphan Song");
            await songRepo.AddAsync(song);

            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await albumService.AddSongToAlbumAsync(song.Id, Guid.NewGuid()));
        }

        // ── AcceptAlbumUploadAsync ──

        [Test]
        public async Task AcceptAlbumUploadAsync_SetsAlbumStatusToApproved()
        {
            var album = CreateAlbum("Pending Album", UploadStatus.Pending);
            await albumRepo.AddAsync(album);

            await albumService.AcceptAlbumUploadAsync(album.Id);

            var updated = await albumRepo.GetByIdAsync(album.Id);
            Assert.That(updated!.Status, Is.EqualTo(UploadStatus.Approved));
        }

        [Test]
        public async Task AcceptAlbumUploadAsync_ApprovesSongsInAlbum()
        {
            var album = CreateAlbum("Pending Album", UploadStatus.Pending);
            await albumRepo.AddAsync(album);

            var song = CreateSong("Pending Song", UploadStatus.Pending);
            song.AlbumId = album.Id;
            await songRepo.AddAsync(song);

            await albumService.AcceptAlbumUploadAsync(album.Id);

            var updatedSong = await songRepo.GetByIdAsync(song.Id);
            Assert.That(updatedSong!.Status, Is.EqualTo(UploadStatus.Approved));
        }

        [Test]
        public void AcceptAlbumUploadAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await albumService.AcceptAlbumUploadAsync(Guid.NewGuid()));
        }

        // ── RejectAlbumUploadAsync ──

        [Test]
        public async Task RejectAlbumUploadAsync_SetsAlbumStatusToRejected()
        {
            var album = CreateAlbum("Pending Album", UploadStatus.Pending);
            await albumRepo.AddAsync(album);

            await albumService.RejectAlbumUploadAsync(album.Id);

            var updated = await albumRepo.GetByIdAsync(album.Id);
            Assert.That(updated!.Status, Is.EqualTo(UploadStatus.Rejected));
        }

        [Test]
        public async Task RejectAlbumUploadAsync_RejectsSongsInAlbum()
        {
            var album = CreateAlbum("Pending Album", UploadStatus.Pending);
            await albumRepo.AddAsync(album);

            var song = CreateSong("Pending Song", UploadStatus.Pending);
            song.AlbumId = album.Id;
            await songRepo.AddAsync(song);

            await albumService.RejectAlbumUploadAsync(album.Id);

            var updatedSong = await songRepo.GetByIdAsync(song.Id);
            Assert.That(updatedSong!.Status, Is.EqualTo(UploadStatus.Rejected));
        }

        [Test]
        public void RejectAlbumUploadAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await albumService.RejectAlbumUploadAsync(Guid.NewGuid()));
        }

        // ── GetTotalAlbumsCountAsync ──

        [Test]
        public async Task GetTotalAlbumsCountAsync_ExcludesDeletedAlbums()
        {
            await albumRepo.AddAsync(CreateAlbum("Approved", UploadStatus.Approved));
            await albumRepo.AddAsync(CreateAlbum("Pending", UploadStatus.Pending));
            await albumRepo.AddAsync(CreateAlbum("Deleted", UploadStatus.Deleted));

            var result = await albumService.GetTotalAlbumsCountAsync();

            Assert.That(result, Is.EqualTo(2));
        }

        // ── GetTotalPendingAlbumsCountAsync ──

        [Test]
        public async Task GetTotalPendingAlbumsCountAsync_ReturnsNonApprovedCount()
        {
            await albumRepo.AddAsync(CreateAlbum("Approved", UploadStatus.Approved));
            await albumRepo.AddAsync(CreateAlbum("Pending", UploadStatus.Pending));
            await albumRepo.AddAsync(CreateAlbum("Rejected", UploadStatus.Rejected));

            var result = await albumService.GetTotalPendingAlbumsCountAsync();

            Assert.That(result, Is.EqualTo(2));
        }

        // ── GetTotalAlbumsCountByCurrentLoggedArtistAsync ──

        [Test]
        public async Task GetTotalAlbumsCountByCurrentLoggedArtistAsync_ReturnsNonDeletedArtistAlbums()
        {
            await CreateAlbumWithArtist("Album 1", UploadStatus.Approved);
            await CreateAlbumWithArtist("Album 2", UploadStatus.Pending);
            await CreateAlbumWithArtist("Deleted Album", UploadStatus.Deleted);

            var result = await albumService.GetTotalAlbumsCountByCurrentLoggedArtistAsync(testUser.Id);

            Assert.That(result, Is.EqualTo(2));
        }

        // ── GetTotalPendingAlbumsCountByCurrentLoggedArtistAsync ──

        [Test]
        public async Task GetTotalPendingAlbumsCountByCurrentLoggedArtistAsync_ReturnsNonApprovedArtistAlbums()
        {
            await CreateAlbumWithArtist("Approved Album", UploadStatus.Approved);
            await CreateAlbumWithArtist("Pending Album", UploadStatus.Pending);

            var result = await albumService.GetTotalPendingAlbumsCountByCurrentLoggedArtistAsync(testUser.Id);

            Assert.That(result, Is.EqualTo(1));
        }
    }
}
