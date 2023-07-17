using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanhiaAguas.Data.Entities
{
    public class Contract : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:hh:mm dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:hh:mm dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? EndDate { get; set; }

        public List<Invoice> Invoice  { get; set;}

        public ContractType ContractType { get; set; }

        public Client Client { get; set; }
    }
}