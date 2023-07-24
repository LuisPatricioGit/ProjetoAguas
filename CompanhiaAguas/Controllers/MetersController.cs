using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompanhiaAguas.Data;
using CompanhiaAguas.Data.Entities;
using CompanhiaAguas.Data.Repositories;

namespace CompanhiaAguas.Controllers
{
    public class MetersController : Controller
    {
        private readonly IMeterRepository _meterRepository;

        public MetersController(IMeterRepository meterRepository)
        {
            _meterRepository = meterRepository;
        }

        // GET: Meters
        public IActionResult Index()
        {
            return View(_meterRepository.GetAll().OrderBy(m => m.Id));
        }

        // GET: Meters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meter = await _meterRepository.GetByIdAsync(id.Value);
            if (meter == null)
            {
                return NotFound();
            }

            return View(meter);
        }

        // GET: Meters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Meter meter)
        {
            if (ModelState.IsValid)
            {
                await _meterRepository.CreateAsync(meter);
                return RedirectToAction(nameof(Index));
            }
            return View(meter);
        }

        // GET: Meters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meter = await _meterRepository.GetByIdAsync(id.Value);
            if (meter == null)
            {
                return NotFound();
            }
            return View(meter);
        }

        // POST: Meters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Meter meter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _meterRepository.UpdateAsync(meter);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _meterRepository.ExistAsync(meter.Id))
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
            return View(meter);
        }

        // GET: Meters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meter = await _meterRepository.GetByIdAsync(id.Value);
            if (meter == null)
            {
                return NotFound();
            }

            return View(meter);
        }

        // POST: Meters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meter = await _meterRepository.GetByIdAsync(id);

            try
            {
                await _meterRepository.DeleteAsync(meter);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{meter.Id} provavelmente está a ser usado!!";
                    ViewBag.ErrorMessage = $"{meter.Id} não pode ser apagado visto a haverem Clientes que o usam. </br></br>" +
                        $"Experimente primeiro apagar todas os Clientes que o estão a usar," +
                        $"e torne novamente a apagá-lo";
                }

                return View("Error");
            }
        }

        public IActionResult MeterNotFound()
        {
            return View();
        }
    }
}
