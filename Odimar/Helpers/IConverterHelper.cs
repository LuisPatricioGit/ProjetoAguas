using Odimar.Data.Entities;
using Odimar.Models;
using System;

namespace Odimar.Helpers
{
    public interface IConverterHelper
    {
        Product ToProduct(ProductViewModel model, Guid imageId, bool isNew);
        ProductViewModel ToProductViewModel(Product product);
    }
}
