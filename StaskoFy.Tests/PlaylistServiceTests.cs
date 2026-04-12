using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using StaskoFy.Core.IService;
using StaskoFy.Core.Service;
using StaskoFy.DataAccess;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.Models.Enums;
using StaskoFy.ViewModels.Playlist;

namespace StaskoFy.Tests
{
    [TestFixture]
    public class PlaylistServiceTests
    {
        private StaskoFyDbContext context;
        private Repository<Playlist> playlistRepo;
        private Repository<Song> songRepo;
        private Repository<PlaylistSong> playlistSongRepo;
        private Mock<IUploadService> mockImageService;
        private PlaylistService playlistService;

        private User testUser;
        private Genre testGenre;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<StaskoFyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new StaskoFyDbContext(options);
            playlistRepo = new Repository<Playlist>(context);
            songRepo = new Repository<Song>(context);
            playlistSongRepo = new Repository<PlaylistSong>(context);
            mockImageService = new Mock<IUploadService>();

            mockImageService
                .Setup(x => x.UploadImageAsync(It.IsAny<Microsoft.AspNetCore.Http.IFormFile>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(("https://cloudinary.com/test.png", "public-id-123"));

            mockImageService
                .Setup(x => x.DestroyImageAsync(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            playlistService = new PlaylistService(playlistRepo, songRepo, playlistSongRepo, mockImageService.Object);

            // Seed
            testUser = new User { Id = Guid.NewGuid(), UserName = "TestUser", Email = "test@test.com", ImageURL = "/img/test.png" };
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

        private Song CreateSong(string title = "Test Song")
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
                Status = UploadStatus.Approved,
                Length = new TimeSpan(0, 3, 30)
            };
        }

        private Playlist CreatePlaylist(string title = "My Playlist", Guid? userId = null)
        {
            return new Playlist
            {
                Id = Guid.NewGuid(),
                Title = title,
                DateCreated = DateOnly.FromDateTime(DateTime.Now),
                UserId = userId ?? testUser.Id,
                ImageURL = "/img/playlist.png",
                CloudinaryPublicId = "pl-test",
                IsPublic = false,
                Length = TimeSpan.Zero
            };
        }

        // ── GetPlaylistsFromCurrentLoggedUserAsync ──

        [Test]
        public async Task GetPlaylistsFromCurrentLoggedUserAsync_ReturnsUserPlaylists()
        {
            await playlistRepo.AddAsync(CreatePlaylist("Playlist 1"));
            await playlistRepo.AddAsync(CreatePlaylist("Playlist 2"));

            var otherUserId = Guid.NewGuid();
            context.Users.Add(new User { Id = otherUserId, UserName = "Other", Email = "other@t.com", ImageURL = "/img/o.png" });
            await context.SaveChangesAsync();
            await playlistRepo.AddAsync(CreatePlaylist("Other Playlist", otherUserId));

            var result = (await playlistService.GetPlaylistsFromCurrentLoggedUserAsync(testUser.Id)).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetPlaylistsFromCurrentLoggedUserAsync_ReturnsEmptyForNewUser()
        {
            var result = (await playlistService.GetPlaylistsFromCurrentLoggedUserAsync(Guid.NewGuid())).ToList();

            Assert.That(result.Count, Is.EqualTo(0));
        }

        // ── GetPlaylistByIdAsync ──

        [Test]
        public async Task GetPlaylistByIdAsync_ReturnsPlaylistWhenFound()
        {
            var playlist = CreatePlaylist("Test Playlist");
            await playlistRepo.AddAsync(playlist);

            var result = await playlistService.GetPlaylistByIdAsync(playlist.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Title, Is.EqualTo("Test Playlist"));
        }

        [Test]
        public void GetPlaylistByIdAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await playlistService.GetPlaylistByIdAsync(Guid.NewGuid()));
        }

        // ── GetTotalPlaylistSongsPagesAsync ──

        [Test]
        public async Task GetTotalPlaylistSongsPagesAsync_ReturnsCorrectPageCount()
        {
            var playlist = CreatePlaylist();
            await playlistRepo.AddAsync(playlist);

            for (int i = 0; i < 7; i++)
            {
                var song = CreateSong($"Song {i}");
                await songRepo.AddAsync(song);
                await playlistSongRepo.AddAsync(new PlaylistSong
                {
                    Id = Guid.NewGuid(),
                    PlaylistId = playlist.Id,
                    SongId = song.Id,
                    DateAdded = DateOnly.FromDateTime(DateTime.Now)
                });
            }

            var result = await playlistService.GetTotalPlaylistSongsPagesAsync(playlist.Id, 5);

            Assert.That(result, Is.EqualTo(2)); // 7 / 5 = 1.4 → ceil = 2
        }

        // ── GetPlaylistSongsByIdAsync ──

        [Test]
        public async Task GetPlaylistSongsByIdAsync_ReturnsSongsInPlaylist()
        {
            var playlist = CreatePlaylist();
            await playlistRepo.AddAsync(playlist);

            var song1 = CreateSong("Song A");
            var song2 = CreateSong("Song B");
            await songRepo.AddAsync(song1);
            await songRepo.AddAsync(song2);

            await playlistSongRepo.AddAsync(new PlaylistSong { Id = Guid.NewGuid(), PlaylistId = playlist.Id, SongId = song1.Id, DateAdded = DateOnly.FromDateTime(DateTime.Now) });
            await playlistSongRepo.AddAsync(new PlaylistSong { Id = Guid.NewGuid(), PlaylistId = playlist.Id, SongId = song2.Id, DateAdded = DateOnly.FromDateTime(DateTime.Now) });

            var result = (await playlistService.GetPlaylistSongsByIdAsync(playlist.Id, "", 1, 10)).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetPlaylistSongsByIdAsync_FiltersByName()
        {
            var playlist = CreatePlaylist();
            await playlistRepo.AddAsync(playlist);

            var song1 = CreateSong("Bohemian Rhapsody");
            var song2 = CreateSong("Stairway to Heaven");
            await songRepo.AddAsync(song1);
            await songRepo.AddAsync(song2);

            await playlistSongRepo.AddAsync(new PlaylistSong { Id = Guid.NewGuid(), PlaylistId = playlist.Id, SongId = song1.Id, DateAdded = DateOnly.FromDateTime(DateTime.Now) });
            await playlistSongRepo.AddAsync(new PlaylistSong { Id = Guid.NewGuid(), PlaylistId = playlist.Id, SongId = song2.Id, DateAdded = DateOnly.FromDateTime(DateTime.Now) });

            var result = (await playlistService.GetPlaylistSongsByIdAsync(playlist.Id, "Bohemian", 1, 10)).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
        }

        // ── AddPlaylistAsync ──

        [Test]
        public async Task AddPlaylistAsync_CreatesPlaylistWithDefaultImage()
        {
            var model = new PlaylistCreateViewModel
            {
                Title = "New Playlist",
                IsPublic = true,
                ImageFile = null
            };

            await playlistService.AddPlaylistAsync(model, testUser.Id);

            var playlists = await playlistRepo.GetAllAttached().ToListAsync();
            Assert.That(playlists.Count, Is.EqualTo(1));
            Assert.That(playlists[0].Title, Is.EqualTo("New Playlist"));
            Assert.That(playlists[0].IsPublic, Is.True);
            Assert.That(playlists[0].ImageURL, Is.EqualTo("/images/defaults/default-album-cover-art.png"));
        }

        // ── UpdatePlaylistAsync ──

        [Test]
        public async Task UpdatePlaylistAsync_UpdatesTitleAndVisibility()
        {
            var playlist = CreatePlaylist("Old Title");
            await playlistRepo.AddAsync(playlist);

            var model = new PlaylistEditViewModel
            {
                Id = playlist.Id,
                Title = "New Title",
                IsPublic = true,
                ImageFile = null
            };

            await playlistService.UpdatePlaylistAsync(model, testUser.Id);

            var updated = await playlistRepo.GetByIdAsync(playlist.Id);
            Assert.That(updated!.Title, Is.EqualTo("New Title"));
            Assert.That(updated.IsPublic, Is.True);
        }

        [Test]
        public void UpdatePlaylistAsync_ThrowsWhenNotFound()
        {
            var model = new PlaylistEditViewModel { Id = Guid.NewGuid(), Title = "Test", IsPublic = false };

            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await playlistService.UpdatePlaylistAsync(model, testUser.Id));
        }

        // ── RemovePlaylistAsync ──

        [Test]
        public async Task RemovePlaylistAsync_RemovesPlaylist()
        {
            var playlist = CreatePlaylist();
            await playlistRepo.AddAsync(playlist);

            await playlistService.RemovePlaylistAsync(playlist.Id);

            var result = await playlistRepo.GetByIdAsync(playlist.Id);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void RemovePlaylistAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await playlistService.RemovePlaylistAsync(Guid.NewGuid()));
        }

        [Test]
        public async Task RemovePlaylistAsync_CallsDestroyImageWhenHasCloudinaryId()
        {
            var playlist = CreatePlaylist();
            playlist.CloudinaryPublicId = "my-public-id";
            await playlistRepo.AddAsync(playlist);

            await playlistService.RemovePlaylistAsync(playlist.Id);

            mockImageService.Verify(x => x.DestroyImageAsync("my-public-id"), Times.Once);
        }

        // ── AddSongToPlaylistAsync ──

        [Test]
        public async Task AddSongToPlaylistAsync_AddsSongAndUpdatesLength()
        {
            var playlist = CreatePlaylist();
            await playlistRepo.AddAsync(playlist);

            var song = CreateSong();
            song.Length = new TimeSpan(0, 4, 0);
            await songRepo.AddAsync(song);

            await playlistService.AddSongToPlaylistAsync(playlist.Id, song.Id);

            var playlistSongs = await playlistSongRepo.GetAllAttached().ToListAsync();
            Assert.That(playlistSongs.Count, Is.EqualTo(1));

            var updatedPlaylist = await playlistRepo.GetByIdAsync(playlist.Id);
            Assert.That(updatedPlaylist!.Length, Is.EqualTo(new TimeSpan(0, 4, 0)));
        }

        [Test]
        public async Task AddSongToPlaylistAsync_DoesNotAddDuplicate()
        {
            var playlist = CreatePlaylist();
            await playlistRepo.AddAsync(playlist);

            var song = CreateSong();
            await songRepo.AddAsync(song);

            await playlistService.AddSongToPlaylistAsync(playlist.Id, song.Id);
            await playlistService.AddSongToPlaylistAsync(playlist.Id, song.Id); // Duplicate

            var playlistSongs = await playlistSongRepo.GetAllAttached().ToListAsync();
            Assert.That(playlistSongs.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddSongToPlaylistAsync_ThrowsWhenSongNotFound()
        {
            var playlist = CreatePlaylist();
            playlistRepo.AddAsync(playlist).Wait();

            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await playlistService.AddSongToPlaylistAsync(playlist.Id, Guid.NewGuid()));
        }

        // ── RemoveSongFromPlaylistAsync ──

        [Test]
        public async Task RemoveSongFromPlaylistAsync_RemovesSongAndUpdatesLength()
        {
            var playlist = CreatePlaylist();
            playlist.Length = new TimeSpan(0, 4, 0);
            await playlistRepo.AddAsync(playlist);

            var song = CreateSong();
            song.Length = new TimeSpan(0, 4, 0);
            await songRepo.AddAsync(song);

            await playlistSongRepo.AddAsync(new PlaylistSong
            {
                Id = Guid.NewGuid(),
                PlaylistId = playlist.Id,
                SongId = song.Id,
                DateAdded = DateOnly.FromDateTime(DateTime.Now)
            });

            await playlistService.RemoveSongFromPlaylistAsync(playlist.Id, song.Id);

            var playlistSongs = await playlistSongRepo.GetAllAttached().ToListAsync();
            Assert.That(playlistSongs.Count, Is.EqualTo(0));

            var updatedPlaylist = await playlistRepo.GetByIdAsync(playlist.Id);
            Assert.That(updatedPlaylist!.Length, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public async Task RemoveSongFromPlaylistAsync_DoesNothingWhenSongNotInPlaylist()
        {
            var playlist = CreatePlaylist();
            await playlistRepo.AddAsync(playlist);

            // Should not throw
            await playlistService.RemoveSongFromPlaylistAsync(playlist.Id, Guid.NewGuid());
        }

        // ── SelectPlaylistsFromCurrentLoggedUserAsync ──

        [Test]
        public async Task SelectPlaylistsFromCurrentLoggedUserAsync_ReturnsUserPlaylists()
        {
            await playlistRepo.AddAsync(CreatePlaylist("PL 1"));
            await playlistRepo.AddAsync(CreatePlaylist("PL 2"));

            var result = (await playlistService.SelectPlaylistsFromCurrentLoggedUserAsync(testUser.Id)).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        // ── GetTotalPlaylistsCountByCurrentLoggedUserAsync ──

        [Test]
        public async Task GetTotalPlaylistsCountByCurrentLoggedUserAsync_ReturnsCorrectCount()
        {
            await playlistRepo.AddAsync(CreatePlaylist("PL 1"));
            await playlistRepo.AddAsync(CreatePlaylist("PL 2"));
            await playlistRepo.AddAsync(CreatePlaylist("PL 3"));

            var result = await playlistService.GetTotalPlaylistsCountByCurrentLoggedUserAsync(testUser.Id);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public async Task GetTotalPlaylistsCountByCurrentLoggedUserAsync_ReturnsZeroForNewUser()
        {
            var result = await playlistService.GetTotalPlaylistsCountByCurrentLoggedUserAsync(Guid.NewGuid());

            Assert.That(result, Is.EqualTo(0));
        }
    }
}
