using System.ComponentModel.DataAnnotations;

namespace StaskoFy.ViewModels.Genre
{
    public class GenreCreateViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Genre's name")]
        public string Name { get; set; }
    }
}
