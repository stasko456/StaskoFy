using Microsoft.EntityFrameworkCore;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Artist;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<ArtistViewModel>> GetAllAsync()
        {
            return await artistRepo.GetAllAttached()
                .Select(a => new ArtistViewModel
                {
                    Id = a.Id,
                    UserId = a.UserId
                }).ToListAsync();
        }

        public async Task<ArtistViewModel?> GetByIdAsync(Guid id)
        {
            var artist = await artistRepo.GetByIdAsync(id);

            if (artist == null)
            {
                throw new KeyNotFoundException("Artist not found.");
            }

            return new ArtistViewModel
            {
                Id = artist.Id,
                UserId = artist.UserId
            };
        }

        public async Task AddAsync(ArtistViewModel model)
        {
            var artist = new Artist
            {
                UserId = model.UserId,
            };

            await artistRepo.AddAsync(artist);
        }

        public async Task RemoveAsync(Guid id)
        {
            var artist = await artistRepo.GetByIdAsync(id);

            await artistRepo.RemoveAsync(artist);
        }

        public async Task<IEnumerable<ArtistSelectViewModel>> PopulateArtistSelectListAsync(Guid userId)
        {
            var artists = await artistRepo.GetAllAttached()
                .Where(a => a.UserId != userId)
                .Select(x => new ArtistSelectViewModel
                {
                    Id = x.Id,
                    Username = x.User.UserName
                }).ToListAsync();

            return artists;
        }
    }
}
