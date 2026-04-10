using StaskoFy.ViewModels.LikedSongs;
using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace StaskoFy.Core.IService
{
    public interface ISongService
    {
        Task<int> GetTotalPendingPagesAsync(int pageSize = 5);

        Task<IEnumerable<SongApprovalViewModel>> FilterSongsWithPendingStatusAsync(string name, int pageNumber = 1, int pageSize = 5);

        Task<SongDetailsViewModel?> GetSongDetailsByIdAsync(Guid id, Guid userId);

        Task<SongEditViewModel?> GetSongByIdAsync(Guid id); 

        Task AddSongAsync(SongCreateViewModel model, Guid userId);

        Task UpdateSongsAsync(SongEditViewModel model, Guid userId);

        Task RemoveSongAsync(Guid id);

        Task<int> GetTotalPagesAsync(int pageSize = 12);

        Task<IEnumerable<SongIndexViewModel>> FilterSongsAsync(string searchItem, List<string> filters, int pageNumber = 1, int pageSize = 12);

        Task<IEnumerable<SongIndexViewModel>> FilterSongsForCurrentLoggedArtistAsync(Guid userId, string searchItem, List<string> filters);

        Task<IEnumerable<SongSelectViewModel>> SelectSinglesByCurrentLoggedArtistAsync(Guid userId);

        Task<IEnumerable<SongIndexViewModel>> GetSinglesForCurrentLoggedArtistAsync(Guid userId);

        Task AcceptSongUploadAsync(Guid id);

        Task RejectSongUploadAsync(Guid id);

        Task<int> GetTotalSongsCountAsync();

        Task<int> GetTotalPendingSongsCountAsync();

        Task<int> GetTotalSongsCountByCurrentLoggedArtistAsync(Guid userId);

        Task<int> GetTotalPendingSongsCountByCurrentLoggedArtistAsync(Guid userId);

        Task<int> GetTotalSongsLikesByCurrentLoggedArtistAsync(Guid userId);

        Task<MostLikedSongViewModel?> GetMostLikedSongAsync(Guid userId);

        Task<SongDetailsForMusicPlayer?> GetSongDetailsForMusicPlayerAsync(Guid id);

        Task<IEnumerable<SongDetailsForMusicPlayer>> GetListOfSongDetailsForMusicPlayerForQueueAsync(int offset = 0, int count = 10);
    }
}
