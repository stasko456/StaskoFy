using Microsoft.EntityFrameworkCore;
using StaskoFy.Core.IServices;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Album;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> albumRepo;

        public AlbumService(IRepository<Album> _albumRepo)
        {
            this.albumRepo = _albumRepo;
        }

        public async Task<IEnumerable<AlbumIndexViewModel>> GetAllAsync()
        {
            return await albumRepo.GetAllAttached()
                .Select(a => new AlbumIndexViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Length = a.Length,
                    ReleaseDate = a.ReleaseDate,
                    SongsCount = a.SongsCount,
                    ImageURL = a.ImageURL,
                    Artists = a.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList()
                }).ToListAsync();
        }

        public async Task<AlbumIndexViewModel?> GetByIdAsync(Guid id)
        {
            var album = await albumRepo.GetAllAttached()
                .Include(x => x.Songs)
                .Include(x => x.ArtistsAlbums)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (album == null)
            {
                throw new KeyNotFoundException("Song not found.");
            }

            return new AlbumIndexViewModel
            {
                Id = id,
                Title = album.Title,
                Length = album.Length,
                ReleaseDate = album.ReleaseDate,
                SongsCount = album.SongsCount,
                ImageURL = album.ImageURL,
                Artists = album.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList()
            };
        }

        public Task AddAsync(AlbumIndexViewModel model, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<AlbumCreateViewModel> models)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AlbumEditViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }
    }
}
