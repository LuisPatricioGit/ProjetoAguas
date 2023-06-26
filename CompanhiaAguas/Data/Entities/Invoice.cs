using System;

namespace CompanhiaAguas.Data.Entities
{
    public class Invoice : IEntity
    {
        public int Id { get; set; }

        public double Value { get; set; }

        public DateTime EmissionDate { get; set; }

        public DateTime DueDate { get; set; }

        public double Consumption { get; set; }
    }
}