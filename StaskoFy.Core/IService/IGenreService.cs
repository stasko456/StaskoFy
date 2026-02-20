using StaskoFy.ViewModels.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IService
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreIndexViewModel>> GetGenresAsync();

        Task<GenreIndexViewModel?> GetGenreByIdAsync(Guid id);

        Task AddGenreAsync(GenreCreateViewModel model);

        Task UpdateGenreAsync(GenreEditViewModel model);
        
        Task RemoveGenreAsync(Guid id);
    }
}
