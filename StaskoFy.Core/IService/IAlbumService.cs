using StaskoFy.Models.Enums;
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
        Task<int> GetTotalPendingPagesAsync(int pageSize = 4);

        Task<IEnumerable<AlbumApprovalViewModel>> FilterAlbumsWithPendingStatusAsync(string title, int pageNumber = 1, int pageSize = 4);

        Task<IEnumerable<AlbumIndexViewModel>> GetSpecificArtistAlbumsAsync(Guid userId);

        Task<AlbumEditViewModel?> GetAlbumByIdAsync(Guid id);

        Task<AlbumSongsIndexViewModel?> GetAlbumByIdWithSongsAsync(Guid id);

        Task AddAlbumAsync(AlbumCreateViewModel model, Guid userId);

        Task UpdateAlbumAsync(AlbumEditViewModel model, Guid userId);
        
        Task RemoveAlbumAsync(Guid id);

        Task<int> GetTotalPagesAsync(int pageSize = 4);

        Task<IEnumerable<AlbumIndexViewModel>> FilterAlbumsAsync(string searchItem, List<string> filters, int pageNumber = 1, int pageSize = 4);

        Task<IEnumerable<AlbumIndexViewModel>> FilterAlbumsForCurrentLoggedArtistAsync(Guid userId, string searchItem, List<string> filters);

        Task RemoveSongFromAlbumAsync(Guid songId);

        Task AddSongToAlbumAsync(Guid songId, Guid albumId);

        Task AcceptAlbumUploadAsync(Guid id);

        Task RejectAlbumUploadAsync(Guid id);

        Task<int> GetTotalAlbumsCountAsync();

        Task<int> GetTotalPendingAlbumsCountAsync();

        Task<int> GetTotalAlbumsCountByCurrentLoggedArtistAsync(Guid userId);

        Task<int> GetTotalPendingAlbumsCountByCurrentLoggedArtistAsync(Guid userIds);

        Task<IEnumerable<SongDetailsForMusicPlayer>> GetSongsFromAlbumByIdForMusicPlayerAsync(Guid albumId);
    }
}
