using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Album;
using StaskoFy.ViewModels.Artist;
using StaskoFy.ViewModels.Playlist;
using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Service
{
    public class ArtistService : IArtistService
    {
        private readonly IRepository<Artist> artistRepo;

        public ArtistService(IRepository<Artist> _artistRepo)
        {
            this.artistRepo = _artistRepo;
        }

        public async Task<IEnumerable<ArtistIndexViewModel>> GetFilteredArtistsAsync(Guid userId, string username)
        {
            var query = artistRepo.GetAllAttached()
                .Where(a => a.UserId != userId);

            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(a =>
                EF.Functions.Like(a.User.UserName, $"%{username}%"));
            }

            return await query
                .Select(x => new ArtistIndexViewModel
                {
                    Id = x.Id,
                    Username = x.User.UserName,
                    ProfilePicture = x.User.ImageURL
                }).ToListAsync();
        }

        public async Task<ArtistIndexViewModel?> GetArtistByIdAsync(Guid id)
        {
            var artist = await artistRepo.GetByIdAsync(id);

            if (artist == null)
            {
                return null;
            }

            return new ArtistIndexViewModel
            {
                Id = artist.Id,
                Username = artist.User.UserName,
                ProfilePicture = artist.User.ImageURL
            };
        }

        public async Task AddArtistAsync(ArtistCreateViewModel model)
        {
            var artist = new Artist
            {
                UserId = model.UserId,
            };

            await artistRepo.AddAsync(artist);
        }

        public async Task RemoveArtistAsync(Guid id)
        {
            var artist = await artistRepo.GetByIdAsync(id);

            await artistRepo.RemoveAsync(artist);
        }

        public async Task<IEnumerable<ArtistIndexViewModel>> PopulateArtistSelectListAsync(Guid userId)
        {
            var artists = await artistRepo.GetAllAttached()
                .Where(a => a.UserId != userId)
                .Select(x => new ArtistIndexViewModel
                {
                    Id = x.Id,
                    Username = x.User.UserName,
                    ProfilePicture = x.User.ImageURL
                }).ToListAsync();

            return artists;
        }

        public async Task<ArtistIndexWithProjects?> GetArtistByIdWithProjectsAsync(Guid id)
        {
            return await artistRepo.GetAllAttached()
                .Include(x => x.ArtistsAlbums)
                    .ThenInclude(album => album.Album)
                .Include(x => x.ArtistsSongs)
                    .ThenInclude(song => song.Song)
                .Include(user => user.User)
                .Where(x => x.Id == id)
                .Select(a => new ArtistIndexWithProjects
                {
                    Id = a.Id,
                    Username = a.User.UserName,
                    ProfilePicture = a.User.ImageURL,
                    Singles = a.ArtistsSongs.Where(x => x.Song.AlbumId == null).Select(x => new SongIndexViewModel
                    {
                        Id = x.Song.Id,
                        Title = x.Song.Title,
                        Minutes = x.Song.Length.Minutes,
                        Seconds = x.Song.Length.Seconds,
                        ReleaseDate = x.Song.ReleaseDate,
                        AlbumName = "Single",
                        GenreId = x.Song.GenreId,
                        GenreName = x.Song.Genre.Name,
                        ImageURL = x.Song.ImageURL,
                        Likes = x.Song.Likes,
                        Artists = x.Song.ArtistsSongs.Select(s => s.Artist.User.UserName).ToList()
                    }).ToList(),
                    Albums = a.ArtistsAlbums.Select(x => new AlbumIndexViewModel
                    {
                        Id = x.Album.Id,
                        Title = x.Album.Title,
                        Hours = x.Album.Length.Hours,
                        Minutes = x.Album.Length.Minutes,
                        Seconds = x.Album.Length.Seconds,
                        ReleaseDate = x.Album.ReleaseDate,
                        SongsCount = x.Album.SongsCount,
                        ImageURL = x.Album.ImageURL,
                        Artists = x.Album.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList(),
                    }).ToList(),
                    Playlists = a.User.Playlists.Where(x => x.IsPublic == true)
                    .Select(pl => new PlaylistIndexViewModel
                    {
                        Id = pl.Id,
                        Title = pl.Title,
                        Hours = pl.Length.Hours,
                        Minutes = pl.Length.Minutes,
                        Seconds = pl.Length.Seconds,
                        SongCount = pl.SongCount,
                        DateCreated = pl.DateCreated,
                        ImageURL = pl.ImageURL,
                        IsPublic = pl.IsPublic
                    }).ToList(),
                }).FirstOrDefaultAsync();
        }
    }
}
