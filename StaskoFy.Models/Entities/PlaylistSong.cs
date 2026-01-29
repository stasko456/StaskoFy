using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Models.Entities
{
    public class PlaylistSong
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(Playlist))]
        public Guid PlaylistId { get; set; }
        public Playlist Playlist { get; set; }

        [Required]
        [ForeignKey(nameof(Song))]
        public Guid SongId { get; set; }
        public Song Song { get; set; }

        [Required]
        public DateOnly DateAdded { get; set; }
    }
}
