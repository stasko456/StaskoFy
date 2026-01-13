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

namespace StaskoFy.Core.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IRepository<Artist> repository;

        public ArtistService(IRepository<Artist> _repository)
        {
            this.repository = _repository;
        }

        public async Task AddAsync(Artist artist)
        {
            await repository.AddAsync(artist);
        }

        public IQueryable<Artist> GetAll()
        {
            return repository.GetAll();
        }

        public async Task<Artist> GetByIdAsync(Guid? id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task RemoveAsync(Artist artist)
        {
            await repository.RemoveAsync(artist);
        }

        public async Task UpdateAsync(Artist artist)
        {
            await repository.UpdateAsync(artist);
        }
    }
}