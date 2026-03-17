using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Song
{
    public class SongIndexViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string ImageURL { get; set; } = null!;
    }
}
