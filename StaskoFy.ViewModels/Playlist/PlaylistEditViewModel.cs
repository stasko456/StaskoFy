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

        [Required(ErrorMessage = "Playlist title is required!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Album title must be between 1 and 100 characters.")]
        public string Title { get; set; } = null!;

        [Required]
        public int Hours { get; set; }

        [Required]
        public int Minutes { get; set; }
        
        [Required]
        public int Seconds { get; set; }

        [Required]
        public int SongCount { get; set; }

        [Required]
        public DateOnly DateCreated { get; set; }

        [Required(ErrorMessage = "Playlist cover is required!")]
        [StringLength(2048)]
        public string ImageURL { get; set; } = "/images/defaults/default_album_cover.png";

        public bool IsPublic { get; set; } = false; // false by default

        public List<Guid> SelectedSongIds { get; set; } = new();
        public Microsoft.AspNetCore.Mvc.Rendering.MultiSelectList? Songs { get; set; }
    }
}
