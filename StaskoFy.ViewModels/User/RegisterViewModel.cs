using System.ComponentModel.DataAnnotations;

namespace StaskoFy.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "EmailAddress")]
        [StringLength(60, MinimumLength = 10)]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(20, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}
