using Microsoft.EntityFrameworkCore;
using StaskoFy.Core.IServices;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Genre;

namespace StaskoFy.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> genreRepo;

        public GenreService(IRepository<Genre> _genreRepo)
        {
            this.genreRepo = _genreRepo;
        }

        public async Task<IEnumerable<GenreIndexViewModel>> GetAllAsync()
        {
            return await genreRepo.GetAllAttached()
                .Select(g => new GenreIndexViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                }).ToListAsync();
        }

        public async Task<GenreIndexViewModel?> GetByIdAsync(Guid id)
        {
            var genre = await genreRepo.GetByIdAsync(id);

            return new GenreIndexViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
            };
        }

        public async Task AddAsync(GenreCreateViewModel model)
        {
            var genre = new Genre
            {
                Name = model.Name,
            };

            await genreRepo.AddAsync(genre);
        }

        public async Task UpdateAsync(GenreEditViewModel model)
        {
            var genre = await genreRepo.GetByIdAsync(model.Id);

            genre.Name = model.Name;

            await genreRepo.UpdateAsync(genre);
        }

        public async Task RemoveAsync(Guid id)
        {
            var genre = await genreRepo.GetByIdAsync(id);

            await genreRepo.RemoveAsync(genre);
        }
    }
}