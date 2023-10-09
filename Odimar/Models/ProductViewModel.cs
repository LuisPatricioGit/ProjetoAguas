using Microsoft.AspNetCore.Http;
using Odimar.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Odimar.Models
{
    public class ProductViewModel : Product
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

    }
}
