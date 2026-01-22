using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Song
{
    public class SongIndexViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Song's Title")]
        public string Title { get; set; }

        [Display(Name = "Song's Length")]
        public TimeSpan Length { get; set; }

        [Display(Name = "Album")]
        public string? AlbumName { get; set; }
        public Guid? AlbumId { get; set; }

        [Display(Name = "Song's Genre")]
        public string GenreName { get; set; }
        public Guid GenreId { get; set; }

        [Display(Name = "Song's Release Date")]
        public DateOnly ReleaseDate { get; set; }

        [Display(Name = "Song's Art Cover")]
        public string ImageURL { get; set; }

        [Display(Name = "Song's Likes")]
        public int Likes { get; set; }

        public List<string> Artists { get; set; } = new List<string>();
    }
}
