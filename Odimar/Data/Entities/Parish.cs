using System.ComponentModel.DataAnnotations;

namespace Odimar.Data.Entities
{
    public class Parish
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Parish")]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters.")]
        public string Name { get; set; }
    }
}
