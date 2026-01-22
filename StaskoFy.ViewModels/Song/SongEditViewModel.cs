using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Song
{
    public class SongEditViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Song's Title is required!")]
        [Display(Name = "Title")]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Song's Length is required!")]
        [Display(Name = "Length")]
        [Range(10, 1200)]
        public TimeSpan Length { get; set; }

        [Required(ErrorMessage = "Song's Release Date is required!")]
        [Display(Name = "Release Date")]
        public DateOnly ReleaseDate { get; set; }

        [Required(ErrorMessage = "Song's Genre is required!")]
        [Display(Name = "Genre")]
        public Guid GenreId { get; set; }

        [Required(ErrorMessage = "Song's ArtCover is required!")]
        [Display(Name = "Art Cover")]
        public string ImageURL { get; set; }
    }
}