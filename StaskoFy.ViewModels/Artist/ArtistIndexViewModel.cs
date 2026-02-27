using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Artist
{
    public class ArtistIndexViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string ProfilePicture { get; set; }
    }
}
