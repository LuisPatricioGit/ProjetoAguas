﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Odimar.Data.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(50, ErrorMessage = "Caracteres demais")]
        public string Name { get; set; }


        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }


        [Display(Name = "Image")]
        public Guid ImageId { get; set; }


        [Display(Name = "Last Purchase")]
        public DateTime? LastPurchase { get; set; }

        [Display(Name = "Last Sale")]
        public DateTime? LastSale { get; set; }


        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }


        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Stock { get; set; }


        public User User { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
            ? $"/images/noimage.png" //"http://luispatricio.somee.com/images/noimage.png"
            : $"https://supershopsi.blob.core.windows.net/products/{ImageId}";

    }
}
