using StaskoFy.ViewModels.Playlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IService
{
    public interface IPlaylistService
    {
        Task<IEnumerable<PlaylistIndexViewModel>> GetAllFromCurrentLoggedUser(Guid userId);

        Task<PlaylistIndexViewModel?> GetByIdAsync(Guid id);

        Task AddAsync(PlaylistCreateViewModel model, Guid userId);

        Task UpdateAsync(PlaylistEditViewModel model);

        Task RemoveAsync(Guid id);
    }
}
