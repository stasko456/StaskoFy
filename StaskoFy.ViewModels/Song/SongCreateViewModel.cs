using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StaskoFy.ViewModels.Song
{
    public class SongCreateViewModel
    {
        [Required(ErrorMessage = "Title is required!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Song's title must be between 1 and 100 characters long.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Minutes are required!")]
        [Range(0, 59, ErrorMessage = "Minutes must be between 0 and 59.")]
        public int Minutes { get; set; }

        [Required(ErrorMessage = "Seconds are required!")]
        [Range(0, 59, ErrorMessage = "Seconds must be between 0 and 59.")]
        public int Seconds { get; set; }

        [Required(ErrorMessage = "Genre is required!")]
        public Guid GenreId { get; set; }

        public IFormFile? ImageFile { get; set; }

        [ValidateNever]
        public List<Guid>? SelectedArtistIds { get; set; } = new();
        public Microsoft.AspNetCore.Mvc.Rendering.MultiSelectList? Artists { get; set; }
    }
}
