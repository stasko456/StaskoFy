using Microsoft.EntityFrameworkCore;
using StaskoFy.Core.IServices;
using StaskoFy.DataAccess.Repository;
using MODELS = StaskoFy.Core.DTOs;
using ENTITIES = StaskoFy.Models.Entities;

namespace StaskoFy.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<ENTITIES.Genre> genreRepo;

        public GenreService(IRepository<ENTITIES.Genre> _genreRepo)
        {
            this.genreRepo = _genreRepo;
        }

        public async Task<IEnumerable<MODELS.Genre>> GetAllAttached()
        {
            return await genreRepo.GetAllAttached()
                .Select(g => new MODELS.Genre
                {
                    Id = g.Id,
                    Name = g.Name,
                    Songs = g.Songs,
                }).ToListAsync();
        }

        public async Task<MODELS.Genre> GetByIdAsync(Guid? id)
        {
            var genre = await genreRepo.GetByIdAsync(id);

            return new MODELS.Genre 
            {
                Id = genre.Id,
                Name = genre.Name,
            };
        }

        public async Task AddAsync(MODELS.Genre model)
        {
            var genre = new ENTITIES.Genre
            {
                Name = model.Name,
            };

            await genreRepo.AddAsync(genre);
        }

        public async Task UpdateAsync(MODELS.Genre model)
        {
            var genre = await genreRepo.GetByIdAsync(model.Id);

            genre.Name = model.Name;

            await genreRepo.UpdateAsync(genre);
        }

        public async Task RemoveAsync(Guid? id)
        {
            var genre = await genreRepo.GetByIdAsync(id);

            await genreRepo.RemoveAsync(genre);
        }
    }
}