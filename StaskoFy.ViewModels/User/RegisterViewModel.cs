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
        [Display(Name = "Username")]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }

        [Required(ErrorMessage = "EmailAddress is required!")]
        [EmailAddress]
        [Display(Name = "EmailAddress")]
        [StringLength(60, MinimumLength = 10)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [Display(Name = "Password")]
        [StringLength(20, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password conformation is required!")]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Selecting a role is required!")]
        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}
