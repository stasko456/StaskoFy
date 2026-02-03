using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StaskoFy.ViewModels.Song
{
    public class SongEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Song title is required!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Song's title must be between 1 and 100 characters long.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Song minutes are required!")]
        [Range(0, 59, ErrorMessage = "Minutes must be between 0 and 59.")]
        public int Minutes { get; set; }

        [Required(ErrorMessage = "Song secnods are required!")]
        [Range(0, 59, ErrorMessage = "Seconds must be between 0 and 59.")]
        public int Seconds { get; set; }

        [Required(ErrorMessage = "Release date is required!")]
        [DataType(DataType.Date)]
        public DateOnly ReleaseDate { get; set; }

        [Required(ErrorMessage = "Genre is required!")]
        public Guid GenreId { get; set; }

        [Required(ErrorMessage = "Song cover is required!")]
        [StringLength(2048)]
        [Url]
        public string ImageURL { get; set; } = null!;

        public List<Guid> SelectedArtistIds { get; set; } = new();

        public Microsoft.AspNetCore.Mvc.Rendering.MultiSelectList? Artists { get; set; }
    }
}
