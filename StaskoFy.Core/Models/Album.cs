using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Models
{
    public class Album
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
       
        public TimeSpan Length { get; set; }

        public DateOnly ReleaseDate { get; set; }

        public int SongsCount { get; set; }

        public string ImageURL { get; set; } = "/wwwroot/images/defaults/default-album-cover-art.png";

        public ICollection<ArtistAlbum> ArtistsAlbums { get; set; } = new List<ArtistAlbum>();

        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }
}
