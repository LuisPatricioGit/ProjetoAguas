using Microsoft.AspNetCore.Mvc.Rendering;
using Odimar.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Odimar.Data
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboProducts();
    }
}
