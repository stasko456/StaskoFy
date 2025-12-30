using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Models.Entities
{
    public class Album
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Album's Title")]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        [Display(Name = "Album's Length")]
        public TimeSpan Length { get; set; }

        [Required]
        [Display(Name = "Album's Release Date")]
        public DateOnly ReleaseDate { get; set; }

        [Display(Name = "Album's count of songs")]
        public int SongsCount { get; set; }

        [Required]
        [Display(Name = "Album's Art Cover")]
        public string ImageURL { get; set; } = "/wwwroot/images/defaults/default-album-cover-art.png";

        public ICollection<ArtistAlbum> ArtistsAlbums { get; set; } = new List<ArtistAlbum>();

        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }
}