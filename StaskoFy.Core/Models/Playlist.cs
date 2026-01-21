using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Models
{
    public class Playlist
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public TimeSpan Length { get; set; }

        public int SongCount { get; set; }

        public DateOnly DataCreated { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public string ImageURL { get; set; } = "/wwwroot/images/defaults/default_album_cover.png";

        public bool IsPublic { get; set; } = false; // false by default

        public ICollection<PlaylistSong> PlaylistsSongs { get; set; } = new List<PlaylistSong>();
    }
}
