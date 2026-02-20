using StaskoFy.ViewModels.Album;
using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Artist
{
    public class ArtistIndexWithSongsAndAlbumsViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string ProfilePicture { get; set; } = null!;

        public List<SongIndexViewModel> Singles { get; set; } = new();

        public List<AlbumSongsIndexViewModel> Albums { get; set; } = new();
    }
}
