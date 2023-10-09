using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Odimar.Data.Entities
{
    public class District : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters.")]
        public string Name { get; set; }

        public ICollection<County> Counties { get; set; }

        [Display(Name = "Number of counties")]
        public int NumberCounties => Counties == null ? 0 : Counties.Count();
    }
}
