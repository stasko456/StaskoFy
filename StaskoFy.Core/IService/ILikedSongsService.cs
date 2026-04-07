using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.LikedSongs;
using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IService
{
    public interface ILikedSongsService
    {
        Task<int> GetTotalPagesAsync(Guid userId, int pageSize = 5);

        Task<IEnumerable<LikedSongsIndexViewModel>> GetLikedSongsFromCurrentLoggedUserAsync(Guid userId, string name, int pageNumber = 1, int pageSize = 5);

        Task<LikedSongs?> GetLikedSongByUserAndSongAsync(Guid userId, Guid songId);

        Task AddLikedSongAsync(LikedSongsCreateViewModel model, Guid userId);

        Task RemoveLikedSongAsync(Guid userId, Guid songId);

        Task<int> GetTotalLikedSongsByCurrentLoggedUserAsync(Guid userId);

        Task<TimeSpan> GetLengthOfLikedSongsByCurrentLoggedUserAsync(Guid userId);

        Task<IEnumerable<SongDetailsForMusicPlayer>> GetLikedSongsByIdForMusicPlayerAsync(Guid userId);
    }
}
