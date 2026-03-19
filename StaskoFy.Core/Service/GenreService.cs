using Microsoft.AspNetCore.Identity;
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
            return await genreRepo.GetAllAttached()
                .Where(g => g.Id == id)
                .Select(g => new GenreIndexViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                }).FirstOrDefaultAsync();
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

        public async Task<int> GetTotalPagesAsync(int pageSize = 5)
        {
            int totalGenres = await genreRepo.GetAllAttached()
                .CountAsync();

            return (int)Math.Ceiling(totalGenres / (double)pageSize);
        }

        public async Task<IEnumerable<GenreIndexViewModel>> FilterGenresAsync(string name, int pageNumber = 1, int pageSize = 5)
        {
            var query = genreRepo.GetAllAttached();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(g => EF.Functions.Like(g.Name, $"%{name}%"));
            }

            return await query
                .Select(g => new GenreIndexViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                }).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
