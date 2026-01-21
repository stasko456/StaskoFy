using StaskoFy.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace StaskoFy.ViewModels.Genre
{
    public class GenreIndexViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Genre's name")]
        public string Name { get; set; }
    }
}