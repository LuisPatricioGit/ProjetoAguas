using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjectoAguasContador.Data.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        [MaxLength(15, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Employee Verified")]
        public bool EmployeeApproved { get; set; }

        [Display(Name = "Admin Verified")]
        public bool AdminApproved { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";
    }
}
