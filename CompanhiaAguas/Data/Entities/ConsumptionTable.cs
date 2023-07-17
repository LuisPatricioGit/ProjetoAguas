using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanhiaAguas.Data.Entities
{
    public class ConsumptionTable : IEntity
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh:mm dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh:mm dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? EndDate { get; set; }

        public List<Tier> Tiers  { get; set; }
    }
}
