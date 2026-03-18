using StaskoFy.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Models.Entities
{
    public class Album
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        // non required because I am calculating the lenght of the songs in the album
        public TimeSpan Length { get; set; }

        // cannot be null
        public DateOnly ReleaseDate { get; set; }

        // non required because I am calculating the count of the osngs in the album
        public int SongsCount { get; set; }

        [Required]
        [MaxLength(2048)]
        public string ImageURL { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string CloudinaryPublicId { get; set; } = null!;

        public UploadStatus Status { get; set; }

        public ICollection<ArtistAlbum> ArtistsAlbums { get; set; } = new List<ArtistAlbum>();

        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }
}
