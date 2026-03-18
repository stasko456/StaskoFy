using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.User
{
    public class EditProfileViewModel
    {
        [Required]
        public string Username { get; set; } = null!;

        public IFormFile? ImageFile { get; set; }

        public string CurrentProfilePicture { get; set; } = null!;
    }
}
