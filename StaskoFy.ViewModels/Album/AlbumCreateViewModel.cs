using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StaskoFy.ViewModels.Album
{
    public class AlbumCreateViewModel
    {
        [Required(ErrorMessage = "Album title is required!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Album title must be between 1 and 100 characters.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Album hours are required!")]
        [Range(0, 59, ErrorMessage = "Hours must be between 0 and 59.")]
        public int Hours { get; set; }

        [Required(ErrorMessage = "Album minutes are required!")]
        [Range(0, 59, ErrorMessage = "Minutes must be between 0 and 59.")]
        public int Minutes { get; set; }

        [Required(ErrorMessage = "Album secnods are required!")]
        [Range(0, 59, ErrorMessage = "Seconds must be between 0 and 59.")]
        public int Seconds { get; set; }

        [Required(ErrorMessage = "Album cover is required!")]
        [StringLength(2048)]
        [Url]
        public string ImageURL { get; set; } = null!;

        public List<Guid> SelectedArtistIds { get; set; } = new();

        public Microsoft.AspNetCore.Mvc.Rendering.MultiSelectList? Artists { get; set; }
    }
}
