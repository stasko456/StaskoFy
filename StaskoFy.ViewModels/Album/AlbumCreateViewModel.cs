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
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Album title must be between 1 and 100 characters.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Album length is required.")]
        public TimeSpan Length { get; set; }

        [Required(ErrorMessage = "Release date is required.")]
        public DateOnly ReleaseDate { get; set; }

        [Required(ErrorMessage = "Album cover is required.")]
        public string ImageURL { get; set; } = null!;

        public ICollection<string> Artists { get; set; } = new List<string>();
        public ICollection<string> Songs { get; set; } = new List<string>();
    }
}
