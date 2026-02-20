using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using Microsoft.EntityFrameworkCore;
using StaskoFy.Core.IService;
 using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
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

        public async Task<IEnumerable<SongIndexViewModel>> GetSpecificArtistSongsAsync(Guid artistId)
        {
            return await songRepo.GetAllAttached()
                .Include(x => x.Genre)
                .Include(x => x.Album)
                .Include(x => x.ArtistsSongs)
                    .ThenInclude(a => a.Artist)
                        .ThenInclude(u => u.User)
                .Where(s => s.ArtistsSongs.Any(a => a.Artist.UserId == artistId))
                .Select(song => new SongIndexViewModel
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
                    CloudinaryPublicId = song.CloudinaryPublicId,
                    Likes = song.Likes,
                    Artists = song.ArtistsSongs.Select(x => x.Artist.User.UserName).ToList()
                })
                .ToListAsync();
        }

        public async Task<SongIndexViewModel?> GetSongByIdAsync(Guid id)
        {
            var song = await songRepo.GetAllAttached().Include(x => x.Genre)
                .Include(x => x.Album)
                .Include(x => x.ArtistsSongs)
                .ThenInclude(x => x.Artist)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (song == null)
            {
                return null;
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
                CloudinaryPublicId = song.CloudinaryPublicId,
                Likes = song.Likes,
                Artists = song.ArtistsSongs.Select(x => x.Artist.User.UserName).ToList()
            };
        }

        public async Task AddSongAsync(SongCreateViewModel model, Guid userId, string imageURL, string publicId)
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
                ImageURL = imageURL,
                CloudinaryPublicId = publicId,
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

        public async Task UpdateSongsAsync(SongEditViewModel model, Guid userId)
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

            // add featured artists to the song if any are selected
            if (featuredArtists.Count > 0)
            {
                // remove ArtistSong for this song from the DB 
                var artistsSong = await artistSongRepo.GetAllAttached()
                    .Where(x => x.SongId == song.Id)
                    .ToListAsync();
                await artistSongRepo.RemoveRangeAsync(artistsSong);

                // add main artist to the song
                song.ArtistsSongs.Add(new ArtistSong
                {
                    ArtistId = mainArtist.Id,
                    SongId = song.Id,
                });

                // add featured artists to the song
                foreach (var artist in featuredArtists)
                {
                    song.ArtistsSongs.Add(new ArtistSong
                    {
                        ArtistId = artist.Id,
                        SongId = song.Id,
                    });
                }
            }

            // update entity
            await songRepo.UpdateAsync(song);
        }

        public async Task RemoveSongAsync(Guid id)
        {
            var song = await songRepo.GetByIdAsync(id);

            await songRepo.RemoveAsync(song);
        }

        public async Task RemoveSongsRangeAsync(IEnumerable<Guid> ids)
        {
            List<Song> songsToRemove = new List<Song>();

            foreach (var id in ids)
            {
                var song = await songRepo.GetByIdAsync(id);

                songsToRemove.Add(song);
            }

            await songRepo.RemoveRangeAsync(songsToRemove);
        }

        public async Task<IEnumerable<SongIndexViewModel>> FilterSongsAsync(string searchItem, List<string> filters)
        {
            var query = songRepo.GetAllAttached();

            if (!string.IsNullOrEmpty(searchItem) && filters != null && filters.Any())
            {
                query = query.Where(s =>
                    (filters.Contains("Title") && EF.Functions.Like(s.Title, $"%{searchItem}%")) ||
                    (filters.Contains("Album") && s.Album != null && EF.Functions.Like(s.Album.Title, $"%{searchItem}%")) ||
                    (filters.Contains("Genre") && EF.Functions.Like(s.Genre.Name, $"%{searchItem}%")) ||
                    (filters.Contains("Artist") && s.ArtistsSongs.Any(a => EF.Functions.Like(a.Artist.User.UserName, $"%{searchItem}%")))
                );
            }

            return await query
                .Select(song => new SongIndexViewModel
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
                    CloudinaryPublicId = song.CloudinaryPublicId,
                    Likes = song.Likes,
                    Artists = song.ArtistsSongs.Select(x => x.Artist.User.UserName).ToList()
                }).ToListAsync();
        }

        public async Task<IEnumerable<SongIndexViewModel>> FilterSongsForCurrentLoggedArtistAsync(Guid userId, string searchItem, List<string> filters)
        {
            var query = songRepo.GetAllAttached()
            .Include(s => s.Genre)
            .Include(s => s.Album)
            .Include(s => s.ArtistsSongs)
                .ThenInclude(x => x.Artist)
                    .ThenInclude(a => a.User)
            .Where(s => s.ArtistsSongs.Any(x => x.Artist.UserId == userId));

            if (!string.IsNullOrEmpty(searchItem) && filters != null && filters.Any())
            {
                query = query.Where(s =>
                    (filters.Contains("Title") && EF.Functions.Like(s.Title, $"%{searchItem}%")) ||
                    (filters.Contains("Album") && s.Album != null && EF.Functions.Like(s.Album.Title, $"%{searchItem}%")) ||
                    (filters.Contains("Genre") && EF.Functions.Like(s.Genre.Name, $"%{searchItem}%"))
                );
            }

            return await query
                .Select(song => new SongIndexViewModel
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
                    CloudinaryPublicId = song.CloudinaryPublicId,
                    Likes = song.Likes,
                    Artists = song.ArtistsSongs.Select(x => x.Artist.User.UserName).ToList()
                }).ToListAsync();
        }

        public async Task<IEnumerable<SongSelectViewModel>> SelectSongsAsync()
        {
            return await songRepo.GetAllAttached()
                .Include(s => s.ArtistsSongs)
                .ThenInclude(x => x.Artist)
                    .ThenInclude(a => a.User)
                .Select(song => new SongSelectViewModel
                {
                    Id = song.Id,
                    Title = song.Title,
                }).ToListAsync();
        }

        public async Task<IEnumerable<SongSelectViewModel>> SelectSongsByCurrentLoggedArtistAsync(Guid userId)
        {
            return await songRepo.GetAllAttached()
                .Include(s => s.ArtistsSongs)
                .ThenInclude(x => x.Artist)
                    .ThenInclude(a => a.User)
                 .Where(s => s.ArtistsSongs.Any(a => a.Artist.UserId == userId))
                .Select(song => new SongSelectViewModel
                {
                    Id = song.Id,
                    Title = song.Title,
                }).ToListAsync();
        }
    }
}
