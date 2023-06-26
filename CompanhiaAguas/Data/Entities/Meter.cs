using System.Collections.Generic;

namespace CompanhiaAguas.Data.Entities
{
    public class Meter : IEntity
    {
        public enum Status
        {
           Pending,
           Normal,
           Damaged
        }
        public int Id { get; set; }

        public double Value { get; set; }
        public int Model { get; set; }

        public Status Status { get; set; }

        public List<SupplyPoint> SupplyPoints { get; set; } 
    }
}