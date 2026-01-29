using Microsoft.EntityFrameworkCore;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Service
{
    public class SongService : ISongService
    {
        private readonly IRepository<Song> songRepo;
        private readonly IRepository<ArtistSong> artistSongRepo;
        private readonly IRepository<Artist> artistRepo;

        public SongService(IRepository<Song> _songRepo, IRepository<ArtistSong> _artistSongRepo, IRepository<Artist> _artistRepo)
        {
            this.songRepo = _songRepo;
            this.artistSongRepo = _artistSongRepo;
            this.artistRepo = _artistRepo;
        }

        public async Task<IEnumerable<SongIndexViewModel>> GetAllAsync()
        {
            return await songRepo.GetAllAttached()
                .Select(s => new SongIndexViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Minutes = s.Length.Minutes,
                    Seconds = s.Length.Seconds,
                    AlbumName = s.Album != null ? s.Album.Title : "Single",
                    AlbumId = s.AlbumId,
                    GenreName = s.Genre.Name,
                    GenreId = s.GenreId,
                    ImageURL = s.ImageURL,
                    Likes = s.Likes,
                    ReleaseDate = s.ReleaseDate,
                    Artists = s.ArtistsSongs.Select(x => x.Artist.User.UserName).ToList()
                }).ToListAsync();
        }

        public async Task<IEnumerable<SongIndexViewModel>> GetSpecificArtistSongsAsync(Guid userId)
        {
            var specificArtistSongs = new List<SongIndexViewModel>();

            var songs = await songRepo.GetAllAttached()
                .Include(x => x.Genre)
                .Include(x => x.Album)
                .Include(x => x.ArtistsSongs)
                    .ThenInclude(a => a.Artist)
                        .ThenInclude(u => u.User)
                .Where(s => s.ArtistsSongs.Any(a => a.Artist.UserId == userId))
                .ToListAsync();

            foreach (var song in songs)
            {
                var vm = new SongIndexViewModel
                {
                    Id = song.Id,
                    Title = song.Title,
                    Minutes = song.Length.Minutes,
                    Seconds = song.Length.Seconds,
                    ReleaseDate = song.ReleaseDate,
                    AlbumName = song.Album != null ? song.Album.Title : "Single",
                    AlbumId = song.AlbumId,
                    GenreId = song.GenreId,
                    GenreName = song.Genre.Name,
                    ImageURL = song.ImageURL,
                    Likes = song.Likes,
                    Artists = song.ArtistsSongs.Select(x => x.Artist.User.UserName).ToList()
                };

                specificArtistSongs.Add(vm);
            }

            return specificArtistSongs;
        }

        public async Task<SongIndexViewModel?> GetByIdAsync(Guid id)
        {
            var song = await songRepo.GetAllAttached().Include(x => x.Genre)
                .Include(x => x.Album)
                .Include(x => x.ArtistsSongs)
                .ThenInclude(x => x.Artist)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (song == null)
            {
                throw new KeyNotFoundException("Song not found.");
            }

            return new SongIndexViewModel
            {
                Id = song.Id,
                Title = song.Title,
                Minutes = song.Length.Minutes,
                Seconds = song.Length.Seconds,
                ReleaseDate = song.ReleaseDate,
                AlbumName = song.Album != null ? song.Album.Title : "Single",
                AlbumId = song.AlbumId,
                GenreId = song.GenreId,
                GenreName = song.Genre.Name,
                ImageURL = song.ImageURL,
                Likes = song.Likes,
                Artists = song.ArtistsSongs.Select(x => x.Artist.User.UserName).ToList()
            };
        }

        public async Task AddAsync(SongCreateViewModel model, Guid userId)
        {
            var mainArtist = await artistRepo.GetAllAttached().
                FirstOrDefaultAsync(x => x.UserId == userId);

            var featuredArtists = await artistRepo.GetAllAttached().
                Where(x => model.SelectedArtistIds.Contains(x.Id))
                .ToListAsync();

            var song = new Song
            {
                Title = model.Title,
                Length = new TimeSpan(0, model.Minutes, model.Seconds),
                ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                GenreId = model.GenreId,
                ImageURL = model.ImageURL,
                ArtistsSongs = new List<ArtistSong>(),
            };

            song.ArtistsSongs.Add(new ArtistSong
            {
                Artist = mainArtist,
                Song = song,
            });

            foreach (var artist in featuredArtists)
            {
                song.ArtistsSongs.Add(new ArtistSong
                {
                    Artist = artist,
                    Song = song,
                });
            }

            await songRepo.AddAsync(song);
        }

        public async Task UpdateAsync(SongEditViewModel model, Guid userId)
        {
            var mainArtist = await artistRepo.GetAllAttached()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            var featuredArtists = await artistRepo.GetAllAttached()
                .Where(x => model.SelectedArtistIds.Contains(model.Id))
                .ToListAsync();

            var song = await songRepo.GetByIdAsync(model.Id);

            song.Title = model.Title;
            song.Length = new TimeSpan(0, model.Minutes, model.Seconds);
            song.ReleaseDate = model.ReleaseDate;
            song.GenreId = model.GenreId;
            song.ImageURL = model.ImageURL;

            await songRepo.UpdateAsync(song);

            // to do
        }

        public async Task RemoveAsync(Guid id)
        {
            var song = await songRepo.GetByIdAsync(id);

            await songRepo.RemoveAsync(song);
        }

        public async Task RemoveRangeAsync(IEnumerable<Guid> ids)
        {
            List<Song> songsToRemove = new List<Song>();

            foreach (var id in ids)
            {
                var song = await songRepo.GetByIdAsync(id);

                songsToRemove.Add(song);
            }

            await songRepo.RemoveRangeAsync(songsToRemove);
        }
    }
}
