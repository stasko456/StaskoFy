using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Artist
{
    public class ArtistCreateViewModel
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
