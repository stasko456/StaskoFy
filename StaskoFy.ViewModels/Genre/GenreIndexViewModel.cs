using StaskoFy.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace StaskoFy.ViewModels.Genre
{
    public class GenreIndexViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}