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

        Task<GenreEditViewModel?> GetGenreByIdAsync(Guid id);

        Task AddGenreAsync(GenreCreateViewModel model);

        Task UpdateGenreAsync(GenreEditViewModel model);
        
        Task RemoveGenreAsync(Guid id);

        Task<int> GetTotalPagesAsync(int pageSize = 5);

        Task<int> GetTotalDeletedPagesAsync(int pageSize = 5);

        Task<IEnumerable<GenreIndexViewModel>> FilterGenresAsync(string name, int pageNumber = 1, int pageSize = 5);

        Task<IEnumerable<GenreApprovalViewModel>> FilterDeletedGenresAsync(string name, int pageNumber = 1, int pageSize = 5);

        Task AcceptGenreUploadAsync(Guid id);

        Task<int> GetGenresCountAsync();

        Task<int> GetDeletedGenresCountAsync();
    }
}
