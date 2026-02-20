using StaskoFy.ViewModels.LikedSongs;
using StaskoFy.ViewModels.Playlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Library
{
    public class LibraryViewModel
    {
        public List<PlaylistIndexViewModel> Playlists { get; set; } = new();

        public List<LikedSongsIndexViewModel> LikedSongs { get; set; } = new();
    }
}
