using StaskoFy.ViewModels.Album;
using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IServices
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumIndexViewModel>> GetAllAsync();

        Task<AlbumIndexViewModel?> GetByIdAsync(Guid id);

        Task AddAsync(AlbumIndexViewModel model, Guid userId);
        Task AddRangeAsync(IEnumerable<AlbumCreateViewModel> models);

        Task RemoveAsync(Guid id);
        Task RemoveRangeAsync(IEnumerable<Guid> ids);

        Task UpdateAsync(AlbumEditViewModel model);
    }
}