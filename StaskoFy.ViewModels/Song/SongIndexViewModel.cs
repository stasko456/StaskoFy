using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Song
{
    public class SongIndexViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public string? AlbumName { get; set; }
        public Guid? AlbumId { get; set; }

        public string GenreName { get; set; } = null!;
        public Guid GenreId { get; set; }

        public DateOnly ReleaseDate { get; set; }

        public string ImageURL { get; set; } = null!;

        public string CloudinaryPublicId { get; set; } = null!;

        public int Likes { get; set; }

        public List<string> Artists { get; set; } = new();
    }
}
