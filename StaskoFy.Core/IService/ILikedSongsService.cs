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
        Task<IEnumerable<LikedSongsIndexViewModel>> GetAllAsync(Guid userId);

        Task AddAsync(LikedSongsCreateViewModel model, Guid userId);

        Task<LikedSongsIndexViewModel?> GetByIdAsync(Guid id, Guid userId);

        Task RemoveAsync(Guid id);
    }
}
