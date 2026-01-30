using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StaskoFy.ViewModels.Song
{
    public class SongCreateViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Song's title must be between 1 and 100 characters long.")]
        public string Title { get; set; } = null!;

        [Range(0, 59, ErrorMessage = "Minutes must be between 0 and 59.")]
        public int Minutes { get; set; }

        [Range(1, 59, ErrorMessage = "Seconds must be between 1 and 59.")]
        public int Seconds { get; set; }

        [Required]
        public Guid GenreId { get; set; }

        [Required]
        [StringLength(2048)]
        public string ImageURL { get; set; } = null!;

        public List<Guid> SelectedArtistIds { get; set; } = new();

        public Microsoft.AspNetCore.Mvc.Rendering.MultiSelectList? Artists { get; set; }
    }
}
