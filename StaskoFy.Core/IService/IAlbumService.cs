using StaskoFy.ViewModels.Album;
using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IService
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumApprovalViewModel>> GetAlbumsWithPendingStatusAsync();

        Task<IEnumerable<AlbumIndexViewModel>> GetSpecificArtistAlbumsAsync(Guid userId);

        Task<AlbumIndexViewModel?> GetAlbumByIdAsync(Guid id);

        Task<AlbumSongsIndexViewModel?> GetAlbumByIdWithSongsAsync(Guid id);

        Task AddAlbumAsync(AlbumCreateViewModel model, Guid userId);

        Task UpdateAlbumAsync(AlbumEditViewModel model, Guid userId);
        
        Task RemoveAlbumAsync(Guid id);

        Task<IEnumerable<AlbumIndexViewModel>> FilterAlbumsAsync(string searchItem, List<string> filters);

        Task<IEnumerable<AlbumIndexViewModel>> FilterAlbumsForCurrentLoggedArtistAsync(Guid userId, string searchItem, List<string> filters);

        Task AcceptAlbumUploadAsync(Guid id);

        Task RejectAlbumUploadAsync(Guid id);
    }
}
