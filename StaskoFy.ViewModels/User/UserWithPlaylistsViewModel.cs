using Microsoft.EntityFrameworkCore.Update.Internal;
using StaskoFy.ViewModels.Playlist;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.User
{
    public class UserWithPlaylistsViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string ProfilePicture { get; set; } = null!;

        public List<PlaylistIndexViewModel> Playlists { get; set; } = new();
    }
}
