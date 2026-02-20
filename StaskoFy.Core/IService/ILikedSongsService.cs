using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.LikedSongs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IService
{
    public interface ILikedSongsService
    {
        Task<IEnumerable<LikedSongsIndexViewModel>> GetLikedSongsFromCurrentLoggedUserAsync(Guid userId);

        Task<LikedSongsIndexViewModel?> GetLikedSongByIdAsync(Guid id);

        Task<LikedSongs?> GetLikedSongByUserAndSongAsync(Guid userId, Guid songId);

        Task AddLikedSongAsync(LikedSongsCreateViewModel model, Guid userId);

        Task RemoveLikedSongAsync(Guid userId, Guid songId);
    }
}
