using StaskoFy.ViewModels.Playlist;
using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Pagination
{
    public class PlaylistSongsPaginationViewModel
    {
        public PlaylistIndexViewModel PlaylistInfo { get; set; } = null!;

        public List<SongPlaylistIndexViewModel> Songs { get; set; } = new();

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
