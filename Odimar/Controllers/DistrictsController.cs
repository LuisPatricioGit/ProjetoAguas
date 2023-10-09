using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odimar.Data;
using Odimar.Data.Entities;
using Odimar.Models;
using System.Threading.Tasks;

namespace Odimar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DistrictsController : Controller
    {
        private readonly IDistrictRepository _districtRepository;

        public DistrictsController(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }
        public IActionResult Index()
        {
            return View(_districtRepository.GetDistrictsWithCounties());
        }

        #region CRUD District
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(District district)
        {
            if (ModelState.IsValid)
            {
                await _districtRepository.CreateAsync(district);
                return RedirectToAction(nameof(Index));
            }
            return View(district);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var district = await _districtRepository.GetDistrictWithCountyAsync(id.Value);
            if (district == null)
            {
                return NotFound();
            }
            return View(district);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var district = await _districtRepository.GetByIdAsync(id.Value);
            if (district == null)
            {
                return NotFound();
            }
            return View(district);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(District district)
        {
            if (ModelState.IsValid)
            {
                await _districtRepository.UpdateAsync(district);
                return RedirectToAction(nameof(Index));
            }
            return View(district);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var district = await _districtRepository.GetByIdAsync(id.Value);
            if (district == null)
            {
                return NotFound();
            }
            await _districtRepository.DeleteAsync(district);
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region CRUD County
        public async Task<IActionResult> AddCounty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var district = await _districtRepository.GetByIdAsync(id.Value);
            if (district == null)
            {
                return NotFound();
            }
            var model = new CountyViewModel
            {
                DistrictId = district.Id,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCounty(CountyViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await _districtRepository.AddCountyAsync(model);
                return RedirectToAction("Details", new { id = model.DistrictId });
            }
            return this.View(model);
        }

        public async Task<IActionResult> DetailsCounty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var county = await _districtRepository.GetCountyWithParishAsync(id.Value);
            if (county == null)
            {
                return NotFound();
            }
            return View(county);
        }

        public async Task<IActionResult> EditCounty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var county = await _districtRepository.GetCountyWithParishAsync(id.Value);
            if (county == null)
            {
                return NotFound();
            }
            return View(county);
        }

        [HttpPost]
        public async Task<IActionResult> EditCounty(County county)
        {
            if (this.ModelState.IsValid)
            {
                var districtId = await _districtRepository.UpdateCountyAsync(county);
                if (districtId != 0)
                {
                    return this.RedirectToAction("Details", new { id = districtId });
                }
            }
            return this.View(county);
        }

        public async Task<IActionResult> DeleteCounty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var county = await _districtRepository.GetCountyWithParishAsync(id.Value);
            if (county == null)
            {
                return NotFound();
            }
            var districtId = await _districtRepository.DeleteCountyAsync(county);
            return this.RedirectToAction("Details", new { id = districtId });
        }
        #endregion

        #region CRUD Parish
        public async Task<IActionResult> AddParish(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var county = await _districtRepository.GetByIdAsync(id.Value);
            if (county == null)
            {
                return NotFound();
            }
            var model = new ParishViewModel
            {
                CountyId = county.Id,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddParish(ParishViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await _districtRepository.AddParishAsync(model);
                return RedirectToAction("Details", new { id = model.ParishId });
            }
            return this.View(model);
        }

        public async Task<IActionResult> EditParish(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var parish = await _districtRepository.GetParishAsync(id.Value);
            if (parish == null)
            {
                return NotFound();
            }
            return View(parish);
        }

        [HttpPost]
        public async Task<IActionResult> EditParish(Parish parish)
        {
            if (this.ModelState.IsValid)
            {
                var countyId = await _districtRepository.UpdateParishAsync(parish);
                if (countyId != 0)
                {
                    return this.RedirectToAction("Details", new { id = countyId });
                }
            }
            return this.View(parish);
        }

        public async Task<IActionResult> DeleteParish(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var parish = await _districtRepository.GetParishAsync(id.Value);
            if (parish == null)
            {
                return NotFound();
            }
            var countyId = await _districtRepository.DeleteParishAsync(parish);
            return this.RedirectToAction("Details", new { id = countyId });
        }
        #endregion
    }
}
