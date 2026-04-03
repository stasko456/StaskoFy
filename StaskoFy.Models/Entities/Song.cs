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
    public class Song
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        public TimeSpan Length { get; set; }

        public DateOnly ReleaseDate { get; set; }

        // can be a single or part of an album
        [ForeignKey(nameof(Album))]
        public Guid? AlbumId { get; set; }
        public Album? Album { get; set; }

        [Required]
        [ForeignKey(nameof(Genre))]
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; } = null!;

        // not required because likes can increase or decrease
        public int Likes { get; set; }

        [Required]
        [MaxLength(2048)]
        public string ImageURL { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string CloudinaryPublicId { get; set; } = null!;

        [Required]
        [MaxLength(2048)]
        public string AudioURL { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string CloudinaryAudioPublicId { get; set; } = null!;

        public UploadStatus Status { get; set; }

        public ICollection<ArtistSong> ArtistsSongs { get; set; } = new List<ArtistSong>();

        public ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();

        public ICollection<LikedSongs> LikedSongs { get; set; } = new List<LikedSongs>();
    }
}
