using StaskoFy.ViewModels.Album;
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
        Task<IEnumerable<PlaylistIndexViewModel>> GetPlaylistsFromCurrentLoggedUserAsync(Guid userId);

        Task<PlaylistIndexViewModel?> GetPlaylistByIdAsync(Guid id);

        Task<PlaylistSongsIndexViewModel?> GetPlaylistByIdWithSongsAsync(Guid id);

        Task AddPlaylistAsync(PlaylistCreateViewModel model, Guid userId);

        Task UpdatePlaylistAsync(PlaylistEditViewModel model, Guid userId);

        Task RemovePlaylistAsync(Guid id);
    }
}
