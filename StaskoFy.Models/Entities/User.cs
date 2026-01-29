using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Models.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string ImageURL { get; set; }

        public ICollection<LikedSongs> LikedSongs { get; set; } = new List<LikedSongs>();

        public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
