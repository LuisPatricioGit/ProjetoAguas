using System.ComponentModel.DataAnnotations;

namespace CompanhiaAguas.Models
{
    public class ChangeUserViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "The Field {0} only can contain {1} characters lenght.")]
        public string Address { get; set; }

        [MaxLength(20, ErrorMessage = "The Field {0} only can contain {1} characters lenght.")]
        public string PhoneNumber { get; set; }
    }
}
