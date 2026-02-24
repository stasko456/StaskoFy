using StaskoFy.ViewModels.Playlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Song
{
    public class SongsPageViewModel
    {
        public List<SongIndexViewModel> Songs { get; set; }

        public List<PlaylistSelectViewModel> Playlists { get; set; }
    }
}
