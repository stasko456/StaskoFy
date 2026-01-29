using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Song
{
    public class SongEditViewModel
    {
        public Guid Id { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Song's title must be between 1 and 100 symbols long!")]
        public string Title { get; set; }

        [Range(0, 59, ErrorMessage = "Minutes must be between 0 and 59.")]
        public int Minutes { get; set; }

        [Range(1, 59, ErrorMessage = "Seconds must be between 1 and 59.")]
        public int Seconds { get; set; }

        [DataType(DataType.Date)]
        public DateOnly ReleaseDate { get; set; }

        public Guid GenreId { get; set; }

        [Required]
        public string ImageURL { get; set; }

        public List<Guid> SelectedArtistIds { get; set; } = new();

        public MultiSelectList? Artists { get; set; }
    }
}
