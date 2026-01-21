using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Models
{
    public class Song
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public TimeSpan Length { get; set; }

        public DateOnly ReleaseDate { get; set; }

        // can be a single or part of an album
        public Guid? AlbumId { get; set; }
        public Album? Album { get; set; }

        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }

        // not required
        public int Likes { get; set; }

        public string ImageURL { get; set; } = "/wwwroot/images/defaults/default-song-cover-art.png";

        public ICollection<ArtistSong> ArtistsSongs { get; set; } = new List<ArtistSong>();

        public ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();
    }
}
