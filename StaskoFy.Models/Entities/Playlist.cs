using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Models.Entities
{
    public class Playlist
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Playlist's Title")]
        public string Title { get; set; }

        [Display(Name = "Playlist's Length")]
        public TimeSpan Length { get; set; }

        [Display(Name = "Playlist's counr of songs")]
        public int SongCount { get; set; }

        [Required]
        [Display(Name = "Created on")]
        public DateOnly DataCreated { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Display(Name = "Playlist's Art Cover")]
        public string ImageURL { get; set; } = "/wwwroot/images/defaults/default_album_cover.png";

        [Display(Name = "Is public")]
        public bool IsPublic { get; set; } = false; // false by default

        public ICollection<PlaylistSong> PlaylistsSongs { get; set; } = new List<PlaylistSong>();
    }
}
