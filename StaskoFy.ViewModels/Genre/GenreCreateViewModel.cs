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
        [Required(ErrorMessage = "Genre's name must be between 1 and 100 symbols long!")]
        [MinLength(1)]
        [MaxLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
