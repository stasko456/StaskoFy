using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.Models.Enums;
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
            return await artistRepo.GetAllAttached()
                .Where(a => a.Id == id)
                .Select(a => new ArtistIndexViewModel
                {
                    Id = a.Id,
                    Username = a.User.UserName,
                    ProfilePicture = a.User.ImageURL
                }).FirstOrDefaultAsync();
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

        public async Task<IEnumerable<ArtistSelectViewModel>> PopulateArtistSelectListAsync(Guid userId)
        {
            return await artistRepo.GetAllAttached()
                .Where(a => a.UserId != userId)
                .Select(x => new ArtistSelectViewModel
                {
                    Id = x.Id,
                    Username = x.User.UserName,
                }).ToListAsync();
        }

        public async Task<ArtistIndexWithProjects?> GetArtistByIdWithProjectsAsync(Guid userId)
        {
            return await artistRepo.GetAllAttached()
                .Where(x => x.UserId == userId)
                .Select(a => new ArtistIndexWithProjects
                {
                    Id = a.Id,
                    Username = a.User.UserName,
                    ProfilePicture = a.User.ImageURL,
                    Singles = a.ArtistsSongs.Where(x => x.Song.AlbumId == null && x.Song.Status == UploadStatus.Approved).Select(x => new SongIndexViewModel
                    {
                        Id = x.Song.Id,
                        Title = x.Song.Title,
                        ImageURL = x.Song.ImageURL,
                    }).ToList(),
                    Albums = a.ArtistsAlbums.Where(x => x.Album.Status == UploadStatus.Approved).Select(x => new AlbumIndexViewModel
                    {
                        Id = x.Album.Id,
                        Title = x.Album.Title,
                        Hours = x.Album.Length.Hours,
                        Minutes = x.Album.Length.Minutes,
                        Seconds = x.Album.Length.Seconds,
                        ReleaseDate = x.Album.ReleaseDate,
                        SongsCount = x.Album.Songs.Count,
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
                        SongCount = pl.PlaylistsSongs.Where(ps => ps.PlaylistId == pl.Id).Count(),
                        DateCreated = pl.DateCreated,
                        ImageURL = pl.ImageURL,
                        IsPublic = pl.IsPublic
                    }).ToList(),
                }).FirstOrDefaultAsync();
        }
    }
}
