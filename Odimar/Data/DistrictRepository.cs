using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Odimar.Data.Entities;
using Odimar.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odimar.Data
{
    public class DistrictRepository : GenericRepository<District>, IDistrictRepository
    {
        private readonly DataContext _context;

        public DistrictRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        #region District
        public async Task<District> GetDistrictAsync(County county)
        {
            return await _context.Districts
                .Where(c => c.Counties.Any(ci => ci.Id == county.Id))
                .FirstOrDefaultAsync();
        }

        public IEnumerable<SelectListItem> GetComboDistricts()
        {
            var list = _context.Districts.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).OrderBy(l => l.Text).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "Select a district",
                Value = "0"
            });
            return list;
        }

        public IQueryable GetDistrictsWithCounties()
        {
            return _context.Districts
                .Include(c => c.Counties)
                .OrderBy(c => c.Name);
        }

        public async Task<District> GetDistrictWithCountyAsync(int id)
        {
            return await _context.Districts
                .Include(c => c.Counties)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }
        #endregion

        #region County
        public async Task AddCountyAsync(CountyViewModel model)
        {
            var district = await this.GetDistrictWithCountyAsync(model.DistrictId);
            if (district == null)
            {
                return;
            }
            district.Counties.Add(new County { Name = model.Name });
            _context.Districts.Update(district);
            await _context.SaveChangesAsync();
        }

        public async Task<County> GetCountyAsync(Parish parish)
        {
            return await _context.Counties
                .Where(p => p.Parishes.Any(pa => pa.Id == parish.Id))
                .FirstOrDefaultAsync();
        }

        public IEnumerable<SelectListItem> GetComboCounties(int districtId)
        {
            var district = _context.Districts.Find(districtId);
            var list = new List<SelectListItem>();
            if (district != null)
            {
                list = _context.Counties.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).OrderBy(l => l.Text).ToList();
                list.Insert(0, new SelectListItem
                {
                    Text = "Select a county",
                    Value = "0"
                });

            }
            return list;
        }

        public async Task<County> GetCountyWithParishAsync(int id)
        {
            return await _context.Counties
                .Include(p => p.Parishes)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public IQueryable GetCountiesWithParishes()
        {
            return _context.Counties
                .Include(p => p.Parishes)
                .OrderBy(p => p.Name);
        }

        public async Task<int> UpdateCountyAsync(County county)
        {
            var district = await _context.Districts
                  .Where(c => c.Counties.Any(ci => ci.Id == county.Id)).FirstOrDefaultAsync();
            if (district == null)
            {
                return 0;
            }
            _context.Counties.Update(county);
            await _context.SaveChangesAsync();
            return district.Id;

        }

        public async Task<int> DeleteCountyAsync(County county)
        {
            var district = await _context.Districts
               .Where(c => c.Counties.Any(ci => ci.Id == county.Id))
               .FirstOrDefaultAsync();
            if (district == null)
            {
                return 0;
            }
            _context.Counties.Remove(county);
            await _context.SaveChangesAsync();
            return district.Id;

        }
        #endregion

        #region Parish

        public async Task AddParishAsync(ParishViewModel model)
        {
            var county = await this.GetCountyWithParishAsync(model.CountyId);
            if (county == null)
            {
                return;
            }
            county.Parishes.Add(new Parish { Name = model.Name });
            _context.Counties.Update(county);
            await _context.SaveChangesAsync();
        }
        public async Task<Parish> GetParishAsync(int id)
        {
            return await _context.Parishes.FindAsync(id);
        }

        public IEnumerable<SelectListItem> GetComboParishes(int districtId, int countyId)
        {
            var district = _context.Districts.Find(districtId);
            var county = _context.Counties.Find(countyId);
            var list = new List<SelectListItem>();
            if (district != null)
            {
                if (county != null)
                {
                    list = _context.Parishes.Select(p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.Id.ToString()
                    }).OrderBy(l => l.Text).ToList();
                    list.Insert(0, new SelectListItem
                    {
                        Text = "Select a parish",
                        Value = "0"
                    });

                }
            }
            return list;
        }

        public async Task<int> UpdateParishAsync(Parish parish)
        {
            var county = await _context.Counties
                  .Where(p => p.Parishes.Any(pa => pa.Id == parish.Id)).FirstOrDefaultAsync();
            if (county == null)
            {
                return 0;
            }
            _context.Parishes.Update(parish);
            await _context.SaveChangesAsync();
            return county.Id;
        }

        public async Task<int> DeleteParishAsync(Parish parish)
        {
            var county = await _context.Counties
               .Where(p => p.Parishes.Any(pa => pa.Id == parish.Id))
               .FirstOrDefaultAsync();
            if (county == null)
            {
                return 0;
            }
            _context.Parishes.Remove(parish);
            await _context.SaveChangesAsync();
            return county.Id;
        }
        #endregion
    }
}
