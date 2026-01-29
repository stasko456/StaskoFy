using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Models.Entities
{
    public class ArtistSong
    {
        [Required]
        [ForeignKey(nameof(Artist))]
        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; }

        [Required]
        [ForeignKey(nameof(Song))]
        public Guid SongId { get; set; }
        public Song Song { get; set; }
    }
}
