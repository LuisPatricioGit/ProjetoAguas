using System;
using System.Collections.Generic;

namespace CompanhiaAguas.Data.Entities
{
    public class Contract : IEntity
    {
        public int Id { get; set; }
        
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<Invoice> Invoice  { get; set;}

        public ContractType ContractType { get; set; }

        public Client Client { get; set; }
    }
}