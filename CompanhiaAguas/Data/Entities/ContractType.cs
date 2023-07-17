using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanhiaAguas.Data.Entities
{
    public class ContractType : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string Name { get; set; }

        public List<Contract> Contracts { get; set; }
    }
}