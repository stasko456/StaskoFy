using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Home
{
    public class HomeArtistIndexViewModel
    {
        public int TotalSongsByArtist { get; set; }

        public int TotalAlbumsByArtist { get; set; }

        public int TotalPendingSongsByArtist { get; set; }

        public int TotalPendingAlbumsByArtist { get; set; }

        public int TotalLikesOfSongsByArtist { get; set; }

        public string? MostLikedSongTitleByArtist { get; set; }

        public int MostLikedSongCountByArtist { get; set; }
    }
}