using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required!")]
        [StringLength(20, MinimumLength = 5, ErrorMessage ="Username must be between 5 and 20 characters!")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Email Address is required!")]
        [EmailAddress]
        [StringLength(60, MinimumLength = 10, ErrorMessage = "Email Address must be between 10 and 60 characters!")]
        public string EmailAddress { get; set; } = null!;

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 20 characters!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Password conformation is required!")]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "Selecting a role is required!")]
        public string Role { get; set; } = null!;
    }
}
