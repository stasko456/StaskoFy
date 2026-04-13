using StaskoFy.Models.Enums;
using StaskoFy.Models.Utilities;
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

        [Range(typeof(TimeSpan), "00:00:00", "04:00:00")]
        public TimeSpan Length { get; set; }

        [Range(typeof(DateOnly), "1900-01-01", "2100-01-01")]
        public DateOnly ReleaseDate { get; set; }

        [Required]
        [MaxLength(2048)]
        public string ImageURL { get; set; } = null!;

        [Required]
        [MaxLength(2048)]
        public string CloudinaryPublicId { get; set; } = null!;

        public UploadStatus Status { get; set; }

        public ICollection<ArtistAlbum> ArtistsAlbums { get; set; } = new List<ArtistAlbum>();

        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }
}
