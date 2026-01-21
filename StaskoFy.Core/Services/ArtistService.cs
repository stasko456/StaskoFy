using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client.Extensibility;
using StaskoFy.Core.IServices;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StaskoFy.ViewModels.Artist;

namespace StaskoFy.Core.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IRepository<Artist> artistRepo;

        public ArtistService(IRepository<Artist> _artistRepo)
        {
            this.artistRepo = _artistRepo;
        }

        public async Task<IEnumerable<ArtistViewModel>> GetAll()
        {
            return await artistRepo.GetAllAttached()
                .Select(a => new ArtistViewModel
                {
                    Id = a.Id,
                    UserId = a.UserId
                }).ToListAsync();
        }

        public async Task<ArtistViewModel> GetByIdAsync(Guid? id)
        {
            var artist = await artistRepo.GetByIdAsync(id);

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

        public async Task RemoveAsync(Guid? id)
        {
            var artist = await artistRepo.GetByIdAsync(id);

            await artistRepo.RemoveAsync(artist);
        }
    }
}