using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Album
{
    public class AlbumCreateViewModel
    {
        [Required]
        [Display(Name = "Title")]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        [Display(Name = "Length")]
        public TimeSpan Length { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateOnly ReleaseDate { get; set; }

        [Display(Name = "Count of songs")]
        public int SongsCount { get; set; }

        [Required]
        [Display(Name = "Art Cover")]
        public string ImageURL { get; set; } = "/images/defaults/default-album-cover-art.png";

        public ICollection<string> Artists { get; set; } = new List<string>();

        public ICollection<string> Songs { get; set; } = new List<string>();
    }
}
