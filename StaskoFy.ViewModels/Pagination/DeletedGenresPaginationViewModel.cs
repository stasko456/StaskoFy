using StaskoFy.ViewModels.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Pagination
{
    public class DeletedGenresPaginationViewModel
    {
        public List<GenreApprovalViewModel> Genres { get; set; } = new();

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
