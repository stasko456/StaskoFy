using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        public string Title { get; set; }

        // non required because I am summing the lenghts of the album's songs
        public TimeSpan Length { get; set; }

        [Required]
        public DateOnly ReleaseDate { get; set; }

        // non required because I am summing the geting the albumSongCount after adding songs to the album
        public int SongsCount { get; set; }

        [Required]
        public string ImageURL { get; set; }

        public ICollection<ArtistAlbum> ArtistsAlbums { get; set; } = new List<ArtistAlbum>();

        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }
}
