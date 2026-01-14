using Microsoft.EntityFrameworkCore;
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
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> genreRepo;

        public GenreService(IRepository<Genre> _genreRepo)
        {
            this.genreRepo = _genreRepo;
        }

        public async Task AddAsync(Genre genre)
        {
            await genreRepo.AddAsync(genre);
        }

        public IQueryable<Genre> GetAll()
        {
            return genreRepo.GetAll().Include(x => x.Songs);
        }

        public async Task<Genre> GetByIdAsync(Guid? id)
        {
            return await genreRepo.GetByIdAsync(id);
        }

        public async Task RemoveAsync(Genre genre)
        {
            await genreRepo.RemoveAsync(genre);
        }

        public async Task UpdateAsync(Genre genre)
        {
            await genreRepo.UpdateAsync(genre);
        }
    }
}