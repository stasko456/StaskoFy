using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Song
{
    public class SongCreateViewModel
    {
        [Required(ErrorMessage = "Song's Title is required!")]
        [Display(Name = "Title")]
        [StringLength(100, MinimumLength = 1)]  
        public string Title { get; set; }

        [Required(ErrorMessage = "Song's Length is required!")]
        [Display(Name = "Length")]
        [Range(10, 1200)]
        public TimeSpan Length { get; set; }

        [Required(ErrorMessage = "Song's Genre is required!")]
        [Display(Name = "Genre")]
        public Guid GenreId { get; set; }

        [Required(ErrorMessage = "Song's ArtCover is required!")]
        [Display(Name = "Art Cover")]
        public string ImageURL { get; set; } = "/wwwroot/images/defaults/default-song-cover-art.png";
    }
}