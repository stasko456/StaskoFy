using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Genre
{
    public class GenreCreateViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Genre's name must be between 1 and 100 characters long.")]
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;
    }
}
