using StaskoFy.ViewModels.Album;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IService
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumIndexViewModel>> GetAllAsync();

        Task<IEnumerable<AlbumIndexViewModel>> GetSpecificArtistAlbumsAsync(Guid userId);

        Task<AlbumIndexViewModel?> GetByIdAsync(Guid id);

        Task AddAsync(AlbumCreateViewModel model, Guid userId);

        Task RemoveAsync(Guid id);
        Task RemoveRangeAsync(IEnumerable<Guid> ids);

        Task UpdateAsync(AlbumEditViewModel model);
    }
}
