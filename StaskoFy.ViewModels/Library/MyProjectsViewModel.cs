using StaskoFy.ViewModels.Album;
using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Library
{
    public class MyProjectsViewModel
    {
        public List<AlbumIndexViewModel> Albums { get; set; }

        public List<SongIndexViewModel> Singles { get; set; }
    }
}
