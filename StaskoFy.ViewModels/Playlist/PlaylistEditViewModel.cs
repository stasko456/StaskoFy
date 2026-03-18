using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Playlist
{
    public class PlaylistEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 100 characters.")]
        public string Title { get; set; } = null!;

        [Required]
        public DateOnly DateCreated { get; set; }

        public IFormFile? ImageFile { get; set; }

        public List<Guid> SelectedSongIds { get; set; } = new();
        public Microsoft.AspNetCore.Mvc.Rendering.MultiSelectList? Songs { get; set; }

        public bool IsPublic { get; set; }
    }
}
