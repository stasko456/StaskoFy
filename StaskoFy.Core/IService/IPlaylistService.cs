using StaskoFy.ViewModels.Album;
using StaskoFy.ViewModels.Playlist;
using StaskoFy.ViewModels.Song;
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

        Task<int> GetTotalPlaylistSongsPagesAsync(Guid id, int pageSize = 5);

        Task<IEnumerable<SongPlaylistIndexViewModel>> GetPlaylistSongsByIdAsync(Guid id, string name, int pageNumber = 1, int pageSize = 5);

        Task<TimeSpan> GetLengthOfPlaylistSongsByIdAsync(Guid playlistId);

        Task<int> GetCountOfPlaylistSongsByIdAsync(Guid playlistId);

        Task AddPlaylistAsync(PlaylistCreateViewModel model, Guid userId);

        Task UpdatePlaylistAsync(PlaylistEditViewModel model, Guid userId);

        Task RemovePlaylistAsync(Guid id);
        
        Task AddSongToPlaylistAsync(Guid playlistId, Guid songId);

        Task RemoveSongFromPlaylistAsync(Guid playlistId, Guid songId);

        Task<IEnumerable<PlaylistSelectViewModel>> SelectPlaylistsFromCurrentLoggedUserAsync(Guid userId);

        Task<int> GetTotalPlaylistsCountByCurrentLoggedUserAsync(Guid userId);

        Task<IEnumerable<SongDetailsForMusicPlayer>> GetSongsFromPlaylistByIdForMusicPlayerAsync(Guid playlistId);
    }
}
