using System;
using System.ComponentModel.DataAnnotations;

namespace Odimar.Models
{
    public class DeliveryViewModel
    {
        public int id { get; set; }

        [Display(Name = "Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }
    }
}
