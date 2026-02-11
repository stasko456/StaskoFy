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

        Task<IEnumerable<AlbumIndexViewModel>> GetSpecificArtistAlbumsAsync(Guid artistId);

        Task<AlbumIndexViewModel?> GetByIdAsync(Guid id);

        Task<AlbumSongsIndexViewModel?> GetByIdWithSongsAsync(Guid id);

        Task AddAsync(AlbumCreateViewModel model, Guid artistId);

        Task RemoveAsync(Guid id);
        Task RemoveRangeAsync(IEnumerable<Guid> ids);

        Task UpdateAsync(AlbumEditViewModel model, Guid artistId);
    }
}
