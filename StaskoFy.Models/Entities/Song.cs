using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Models.Entities
{
    public class Song
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public TimeSpan Length { get; set; }

        [Required]
        public DateOnly ReleaseDate { get; set; }

        // can be a single or part of an album
        [ForeignKey(nameof(Album))]
        public Guid? AlbumId { get; set; }
        public Album? Album { get; set; }

        [Required]
        [ForeignKey(nameof(Genre))]
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }

        // not required
        public int Likes { get; set; }

        public string ImageURL { get; set; }

        public ICollection<ArtistSong> ArtistsSongs { get; set; } = new List<ArtistSong>();

        public ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();
    }
}
