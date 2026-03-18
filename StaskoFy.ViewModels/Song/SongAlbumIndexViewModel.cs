using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Song
{
    public class SongAlbumIndexViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public int Minutes { get; set; }

        public int Seconds { get; set; }

        public string Genre { get; set; } = null!;

        public List<string> Artists { get; set; } = new();
    }
}
