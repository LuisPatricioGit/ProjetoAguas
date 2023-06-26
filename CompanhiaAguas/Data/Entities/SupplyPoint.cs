using System.Collections.Generic;

namespace CompanhiaAguas.Data.Entities
{
    public class SupplyPoint : IEntity
    {
        public int Id { get; set; }

        public string Address { get; set; }
        public string Floor { get; set; }
        public string PostalCode { get; set; }

        public List<Contract> Contracts { get; set; }

        public Meter Meter { get; set; }
    }
}
