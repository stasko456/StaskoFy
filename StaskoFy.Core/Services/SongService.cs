using Microsoft.EntityFrameworkCore;
using StaskoFy.Core.IServices;
using StaskoFy.DataAccess;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Services
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
                    Length = s.Length,
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

        public async Task<IEnumerable<SongIndexViewModel>> GetSpecificArtistSongsAsync (Guid userId)
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
                    Length = song.Length,
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
                Length = song.Length,
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

        public async  Task AddAsync(SongCreateViewModel model, Guid userId)
        {
            var artist = await artistRepo.GetAllAttached().FirstOrDefaultAsync(x => x.UserId == userId);

            var song = new Song
            {
                Title = model.Title,
                Length = model.Length,
                ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                GenreId = model.GenreId,
                ImageURL = model.ImageURL,
            };

            var artistSong = new ArtistSong
            {
                Artist = artist,
                Song = song,
            };

            await artistSongRepo.AddAsync(artistSong);
        }

        public async Task AddRangeAsync(IEnumerable<SongCreateViewModel> models)
        {
            List<Song> songsToAdd = new List<Song>();

            foreach (var model in models)
            {
                var song = new Song
                {
                    Title = model.Title,
                    Length = model.Length,
                    ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                    GenreId = model.GenreId,
                    ImageURL = model.ImageURL,
                };

                songsToAdd.Add(song);
            }

            await songRepo.AddRangeAsync(songsToAdd);
        }

        public async Task UpdateAsync(SongEditViewModel model)
        {
            var song = await songRepo.GetByIdAsync(model.Id);

            song.Title = model.Title;
            song.Length = model.Length;
            song.ReleaseDate = model.ReleaseDate;
            song.GenreId = model.GenreId;
            song.ImageURL = model.ImageURL;

            await songRepo.UpdateAsync(song);
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