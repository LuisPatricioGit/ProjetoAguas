using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CompanhiaAguas.Data.Entities
{
    public class User : IdentityUser
    {
        public enum AccountStatus {
            Pending,
            Terminated,
            Approved,
        };

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FiscalNumber { get; set; }

        public string Address { get; set; }

        public string ProfilePicUrl { get; set;}
        
        public AccountStatus Status { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

    }
}
