using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [MaxLength(100)]
        public string Title { get; set; }

        // non required because I am summing the lenghts of the playlist's songs
        public TimeSpan Length { get; set; }

        // non required because I am summing the songCOunt of the playlist's songs
        public int SongCount { get; set; }

        [Required]
        public DateOnly DateCreated { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public string ImageURL { get; set; }

        public bool IsPublic { get; set; } = false; // false by default

        public ICollection<PlaylistSong> PlaylistsSongs { get; set; } = new List<PlaylistSong>();
    }
}
