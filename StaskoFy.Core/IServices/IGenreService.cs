using Microsoft.EntityFrameworkCore.Infrastructure;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IServices
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreIndexViewModel>> GetAllAsync();
        Task<GenreIndexViewModel> GetByIdAsync(Guid? id);
        Task AddAsync(GenreCreateViewModel model);
        Task RemoveAsync(Guid? id);
        Task UpdateAsync(GenreEditViewModel model);
    }
}