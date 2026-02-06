using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.LikedSongs
{
    public class LikedSongsIndexViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string AlbumTitle { get; set; } = null!;

        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public string ImageUrl { get; set; } = null!;

        public DateOnly DateAdded { get; set; }

        public List<string> Artists { get; set; } = new();
    }
}
