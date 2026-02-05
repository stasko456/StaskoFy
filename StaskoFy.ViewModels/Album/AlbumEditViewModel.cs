using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StaskoFy.ViewModels.Album
{
    public class AlbumEditViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Album title is required!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Album title must be between 1 and 100 characters.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Release date is required.")]
        [DataType(DataType.Date)]
        public DateOnly ReleaseDate { get; set; }

        [Required(ErrorMessage = "Album cover is required.")]
        [StringLength(2048)]
        public string ImageURL { get; set; } = null!;

        public List<Guid> SelectedArtistIds { get; set; } = new();
        public Microsoft.AspNetCore.Mvc.Rendering.MultiSelectList? Artists { get; set; }

        public List<Guid> SelectedSongIds { get; set; } = new();
        public Microsoft.AspNetCore.Mvc.Rendering.MultiSelectList? Songs { get; set; }
    }
}
