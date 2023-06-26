using System.Collections.Generic;

namespace CompanhiaAguas.Data.Entities
{
    public class ContractType : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Contract> Contracts { get; set; }
    }
}