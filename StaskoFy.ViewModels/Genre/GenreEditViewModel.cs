using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Genre
{
    public class GenreEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Genre name is required!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Genre's name must be between 1 and 100 characters long.")]
        public string Name { get; set; } = null!;
    }
}
