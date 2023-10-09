using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Odimar.Data.Entities
{
    public class County : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "County")]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters.")]
        public string Name { get; set; }

        public ICollection<Parish> Parishes { get; set; }

        [Display(Name = "Number of counties")]
        public int NumberParishes => Parishes == null ? 0 : Parishes.Count();
    }
}
