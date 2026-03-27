using Microsoft.AspNetCore.Http;
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
        [Required(ErrorMessage = "Title is required!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 100 characters.")]
        public string Title { get; set; } = null!;

        public IFormFile? ImageFile { get; set; }

        public List<Guid>? SelectedArtistIds { get; set; } = new();
        public Microsoft.AspNetCore.Mvc.Rendering.MultiSelectList? Artists { get; set; }

        [Required(ErrorMessage = "Please select at least one song.")]
        [MinLength(1, ErrorMessage = "Please select at least one song.")]
        [MaxLength(4, ErrorMessage = "Cannot add more than 4 features to an album.")]
        public List<Guid>? SelectedSongIds { get; set; }
        public Microsoft.AspNetCore.Mvc.Rendering.MultiSelectList? Songs { get; set; }
    }
}
