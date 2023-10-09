using Microsoft.AspNetCore.Mvc.Rendering;
using Odimar.Data.Entities;
using Odimar.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odimar.Data
{
    public interface IDistrictRepository : IGenericRepository<District>
    {
        IQueryable GetDistrictsWithCounties();
        IQueryable GetCountiesWithParishes();
        Task<District> GetDistrictWithCountyAsync(int id);
        Task<County> GetCountyWithParishAsync(int id);
        Task<Parish> GetParishAsync(int id);
        Task AddCountyAsync(CountyViewModel model);
        Task AddParishAsync(ParishViewModel model);
        Task<int> UpdateCountyAsync(County county);
        Task<int> UpdateParishAsync(Parish parish);
        Task<int> DeleteCountyAsync(County county);
        Task<int> DeleteParishAsync(Parish parish);
        IEnumerable<SelectListItem> GetComboDistricts();
        IEnumerable<SelectListItem> GetComboCounties(int districtId);
        IEnumerable<SelectListItem> GetComboParishes(int districtId, int countyId);
        Task<District> GetDistrictAsync(County county);
        Task<County> GetCountyAsync(Parish parish);
    }
}
