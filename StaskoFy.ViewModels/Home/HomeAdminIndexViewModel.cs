using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Home
{
    public class HomeAdminIndexViewModel
    {
        public int TotalSongs  { get; set; }

        public int TotalAlbums { get; set; }

        public int TotalPendingSongs { get; set; }

        public int TotalPendingAlbums { get; set; }
    }
}
