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
        [MinLength(1)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Display(Name = "Length")]
        public TimeSpan Length { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateOnly ReleaseDate { get; set; }

        [Display(Name = "Art Cover")]
        public string ImageURL { get; set; }

        public ICollection<string> Artists { get; set; } = new List<string>();

        public ICollection<string> Songs { get; set; } = new List<string>();
    }
}
