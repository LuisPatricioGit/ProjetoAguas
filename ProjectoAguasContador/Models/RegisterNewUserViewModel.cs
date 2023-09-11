using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectoAguasContador.Models
{
    public class RegisterNewUserViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [MaxLength(100, ErrorMessage = "The Field {0} only can contain {1} characters lenght.")]
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        [MaxLength(20, ErrorMessage = "The Field {0} only can contain {1} characters lenght.")]
        public string PhoneNumber { get; set; }

    }
}
