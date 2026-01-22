using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Models.Entities
{
    public class User: IdentityUser<Guid>
    {
        [Display(Name = "Profile Picture")]
        public string ImageURL { get; set; } = "/images/defaults/default-user-pfp.png";

        public ICollection<LikedSongs> LikedSongs { get; set; } = new List<LikedSongs>();

        public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}