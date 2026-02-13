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
        Task<IEnumerable<LikedSongsIndexViewModel>> GetAllFromCurrentLoggedUserAsync(Guid userId);

        Task AddAsync(LikedSongsCreateViewModel model, Guid userId);

        Task<LikedSongsIndexViewModel?> GetByIdAsync(Guid id);

        Task<LikedSongs?> GetByUserAndSongAsync(Guid userId, Guid songId);

        Task RemoveAsync(Guid userId, Guid songId);
    }
}
