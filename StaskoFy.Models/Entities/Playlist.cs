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
        public string Title { get; set; } = null!;

        public TimeSpan Length { get; set; }

        public DateOnly DateCreated { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        [MaxLength(2048)]
        public string ImageURL { get; set; } = null!;

        [Required]
        [MaxLength(2048)]
        public string CloudinaryPublicId { get; set; } = null!;

        public bool IsPublic { get; set; } = false;

        public ICollection<PlaylistSong> PlaylistsSongs { get; set; } = new List<PlaylistSong>();
    }
}
