using Microsoft.EntityFrameworkCore;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Service
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> genreRepo;

        public GenreService(IRepository<Genre> _genreRepo)
        {
            this.genreRepo = _genreRepo;
        }

        public async Task<IEnumerable<GenreIndexViewModel>> GetGenresAsync()
        {
            return await genreRepo.GetAllAttached()
                .Select(g => new GenreIndexViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                }).ToListAsync();
        }

        public async Task<GenreIndexViewModel?> GetGenreByIdAsync(Guid id)
        {
            var genre = await genreRepo.GetByIdAsync(id);

            if (genre == null)
            {
                return null;
            }

            return new GenreIndexViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
            };
        }

        public async Task AddGenreAsync(GenreCreateViewModel model)
        {
            var genre = new Genre
            {
                Name = model.Name,
            };

            await genreRepo.AddAsync(genre);
        }

        public async Task UpdateGenreAsync(GenreEditViewModel model)
        {
            var genre = await genreRepo.GetByIdAsync(model.Id);

            genre.Name = model.Name;

            await genreRepo.UpdateAsync(genre);
        }

        public async Task RemoveGenreAsync(Guid id)
        {
            var genre = await genreRepo.GetByIdAsync(id);

            await genreRepo.RemoveAsync(genre);
        }
    }
}
