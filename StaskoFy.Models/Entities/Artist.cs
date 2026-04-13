using StaskoFy.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Models.Entities
{
    public class Artist
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<ArtistSong> ArtistsSongs { get; set; } = new List<ArtistSong>();

        public ICollection<ArtistAlbum> ArtistsAlbums { get; set; } = new List<ArtistAlbum>();

        public UploadStatus IsAccepted { get; set; }
    }
}
