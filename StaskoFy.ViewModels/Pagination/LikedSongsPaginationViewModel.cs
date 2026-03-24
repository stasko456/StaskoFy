using StaskoFy.ViewModels.LikedSongs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Pagination
{
    public class LikedSongsPaginationViewModel
    {
        public List<LikedSongsIndexViewModel> LikedSongs { get; set; } = new();

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int LikedSongsCount { get; set; }

        public int LikedSongsHours { get; set; }

        public int LikedSongsMinutes { get; set; }

        public int LikedSongsSeconds { get; set; }
    }
}
