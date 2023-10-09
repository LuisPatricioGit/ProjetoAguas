using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Odimar.Models
{
    public class RegisterNewUserViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name = "Surname")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Username { get; set; }

        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters length")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters length")]
        public string PhoneNumber { get; set; }

        [Display(Name = "County")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a county")]
        public int CountyId { get; set; }

        public IEnumerable<SelectListItem> Counties { get; set; }

        [Display(Name = "District")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a district")]
        public int DistrictId { get; set; }

        public IEnumerable<SelectListItem> Districts { get; set; }


        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Confirm")]
        public string ConfirmPassword { get; set; }
    }
}
