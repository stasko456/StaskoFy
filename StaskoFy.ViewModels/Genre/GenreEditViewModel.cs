using System.ComponentModel.DataAnnotations;

namespace StaskoFy.ViewModels.Genre
{
    public class GenreEditViewModel
    {
        [Required]
        [Display(Name = "Genre's Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Genre's name is required!")]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Genre's name")]
        public string Name { get; set; }
    }
}
