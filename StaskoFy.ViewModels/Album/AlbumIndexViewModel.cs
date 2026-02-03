using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Album
{
    public class AlbumIndexViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public DateOnly ReleaseDate { get; set; }

        public int SongsCount { get; set; }

        public string ImageURL { get; set; }

        public List<string> Artists { get; set; } = new List<string>();

        public List<string> Songs { get; set; } = new List<string>();
    }
}
