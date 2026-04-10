using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Playlist
{
    public class PlaylistIndexViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public DateOnly DateCreated { get; set; }

        public string ImageURL { get; set; } = null!;

        public bool IsPublic { get; set; }

        public Guid UserId { get; set; }
    }
}
