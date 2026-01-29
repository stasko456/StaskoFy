using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Models.Entities
{
    public class ArtistAlbum
    {
        [Required]
        [ForeignKey(nameof(Artist))]
        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; }

        [Required]
        [ForeignKey(nameof(Album))]
        public Guid AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
