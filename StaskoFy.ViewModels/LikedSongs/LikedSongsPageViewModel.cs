using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.LikedSongs
{
    public class LikedSongsPageViewModel
    {
        public int SongsCount { get; set; }

        public TimeSpan Length { get; set; }

        public List<LikedSongsIndexViewModel> LikedSongs { get; set; } = new();
    }
}
