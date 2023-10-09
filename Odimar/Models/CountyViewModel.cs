using System.ComponentModel.DataAnnotations;

namespace Odimar.Models
{
    public class CountyViewModel
    {
        public int DistrictId { get; set; }
        public int CountyId { get; set; }

        [Required]
        [Display(Name = "County")]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters.")]
        public string Name { get; set; }
    }
}
