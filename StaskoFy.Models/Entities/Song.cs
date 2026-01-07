using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Song's Title")]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Songs's Length")]
        [Range(10, 1200)] // 10 seconds to 20 minutes
        public TimeSpan Length { get; set; }

        [Required]
        [Display(Name = "Song's Release Date")]
        public DateOnly ReleaseDate { get; set; }

        // can be a single or part of an album
        [ForeignKey(nameof(Album))]
        [Display(Name = "Album")]
        public Guid? AlbumId { get; set; }
        public Album? Album { get; set; }

        [Required]
        [ForeignKey(nameof(Genre))]
        [Display(Name = "Genre")]
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }

        // not required
        [Display(Name = "Song's likes")]
        public int Likes { get; set; }

        [Required]
        [Display(Name = "Song's Art Cover")]
        public string ImageURL { get; set; } = "/wwwroot/images/defaults/default-song-cover-art.png";

        public ICollection<ArtistSong> ArtistsSongs { get; set; } = new List<ArtistSong>();

        public ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();
    }
}
