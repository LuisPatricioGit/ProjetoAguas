using CompanhiaAguas.Data.Entities;
using CompanhiaAguas.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguas.Controllers
{
    public class SupplyPointsController : Controller
    {
        private readonly ISupplyPointRepository _supplyPointRepository;

        public SupplyPointsController(ISupplyPointRepository supplyPointRepository)
        {
            _supplyPointRepository = supplyPointRepository;
        }

        // GET: SupplyPoints
        public IActionResult Index()
        {
            return View(_supplyPointRepository.GetAll().OrderBy(sp => sp.Id));
        }

        // GET: SupplyPoints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyPoint = await _supplyPointRepository.GetByIdAsync(id.Value);
            if (supplyPoint == null)
            {
                return NotFound();
            }

            return View(supplyPoint);
        }

        // GET: SupplyPoints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SupplyPoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplyPoint supplyPoint)
        {
            if (ModelState.IsValid)
            {
                await _supplyPointRepository.CreateAsync(supplyPoint);
                return RedirectToAction(nameof(Index));
            }
            return View(supplyPoint);
        }

        // GET: SupplyPoints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyPoint = await _supplyPointRepository.GetByIdAsync(id.Value);
            if (supplyPoint == null)
            {
                return NotFound();
            }
            return View(supplyPoint);
        }

        // POST: SupplyPoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SupplyPoint supplyPoint)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _supplyPointRepository.UpdateAsync(supplyPoint);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _supplyPointRepository.ExistAsync(supplyPoint.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(supplyPoint);
        }

        // GET: SupplyPoints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyPoint = await _supplyPointRepository.GetByIdAsync(id.Value);
            if (supplyPoint == null)
            {
                return NotFound();
            }

            return View(supplyPoint);
        }

        // POST: SupplyPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplyPoint = await _supplyPointRepository.GetByIdAsync(id);

            try
            {
                await _supplyPointRepository.DeleteAsync(supplyPoint);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{supplyPoint.Id} provavelmente está a ser usado!!";
                    ViewBag.ErrorMessage = $"{supplyPoint.Id} não pode ser apagado visto a haverem Contadores que o usam. </br></br>" +
                        $"Experimente primeiro apagar todas os Contadores que o estão a usar," +
                        $"e torne novamente a apagá-lo";
                }

                return View("Error");
            }
        }

        public IActionResult SupplyPointNotFound()
        {
            return View();
        }
    }
}
