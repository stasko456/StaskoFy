using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Playlist
{
    public class PlaylistIndexViewModel
    {
        public string Title { get; set; } = null!;

        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public int SongCount { get; set; }

        public DateOnly DateCreated { get; set; }

        public string ImageURL { get; set; } = null!;

        public bool IsPublic { get; set; }
    }
}
