using Azure.Identity;
using StaskoFy.ViewModels.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Pagination
{
    public class GenresPaginationViewModel
    {
        public List<GenreIndexViewModel> Genres { get; set; } = new();

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int GenresCount { get; set; }
    }
}
