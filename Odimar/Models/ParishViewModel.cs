using System.ComponentModel.DataAnnotations;

namespace Odimar.Models
{
    public class ParishViewModel
    {
        public int CountyId { get; set; }
        public int ParishId { get; set; }

        [Required]
        [Display(Name = "Parish")]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters.")]
        public string Name { get; set; }
    }
}
