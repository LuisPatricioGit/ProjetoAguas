using System;
using System.Collections.Generic;

namespace CompanhiaAguas.Data.Entities
{
    public class ConsumptionTable : IEntity
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<Tier> Tiers  { get; set; }
    }
}
