using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Pagination
{
    public class PendingSongsViewModel
    {
        public List<SongApprovalViewModel> Songs { get; set; } = new();

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
