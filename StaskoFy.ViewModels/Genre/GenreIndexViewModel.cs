using StaskoFy.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace StaskoFy.ViewModels.Genre
{
    public class GenreIndexViewModel
    {
        [Display(Name = "Genre's Id")]
        public Guid Id { get; set; }

        [Display(Name = "Genre's name")]
        public string Name { get; set; }
    }
}