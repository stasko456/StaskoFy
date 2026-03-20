using StaskoFy.ViewModels.Album;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Pagination
{
    public class PendingAlbumsViewModel
    {
        public List<AlbumApprovalViewModel> Albums { get; set; } = new();

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
