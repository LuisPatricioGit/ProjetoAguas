using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Value { get; set; }

        [Required]
        public int Model { get; set; }

        [Required]
        public Status MeterStatus { get; set; }

        public List<SupplyPoint> SupplyPoints { get; set; } 
    }
}