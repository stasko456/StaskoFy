using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Playlist
{
    public class PlaylistCreateViewModel
    {
        [Required(ErrorMessage = "Title is required!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 100 characters.")]
        public string Title { get; set; } = null!;

        public IFormFile? ImageFile { get; set; }

        public bool IsPublic { get; set; }

        [Required(ErrorMessage = "Please select at least one song.")]
        [MinLength(1, ErrorMessage = "Please select at least one song.")]
        public List<Guid>? SelectedSongIds { get; set; }
        public Microsoft.AspNetCore.Mvc.Rendering.MultiSelectList? Songs { get; set; }
    }
}
