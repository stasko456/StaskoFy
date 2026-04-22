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
        [Required(ErrorMessage = "Username is required!")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 20 characters!")]
        public string Username { get; set; } = null!;

        public IFormFile? ImageFile { get; set; }

        [Required]
        public string CurrentProfilePicture { get; set; } = null!;
    }
}
