using System.ComponentModel.DataAnnotations;

namespace Odimar.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "District")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
