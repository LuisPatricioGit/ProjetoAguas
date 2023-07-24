using CompanhiaAguas.Data.Entities;
using CompanhiaAguas.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguas.Controllers
{
    public class ContractsController : Controller
    {
        private readonly IContractRepository _contractRepository;

        public ContractsController(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        // GET: Contracts
        public IActionResult Index()
        {
            return View(_contractRepository.GetAll().OrderBy(c => c.Client));
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _contractRepository.GetByIdAsync(id.Value);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contract contract)
        {
            if (ModelState.IsValid)
            {
                await _contractRepository.CreateAsync(contract);
                return RedirectToAction(nameof(Index));
            }
            return View(contract);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _contractRepository.GetByIdAsync(id.Value);
            if (contract == null)
            {
                return NotFound();
            }
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Contract contract)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _contractRepository.UpdateAsync(contract);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _contractRepository.ExistAsync(contract.Id))
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
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _contractRepository.GetByIdAsync(id.Value);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _contractRepository.GetByIdAsync(id);

            try
            {
                await _contractRepository.DeleteAsync(contract);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{contract.Id} provavelmente está a ser usado!!";
                    ViewBag.ErrorMessage = $"{contract.Id} não pode ser apagado visto a haverem Clientes que o usam. </br></br>" +
                        $"Experimente primeiro apagar todas os Clientes que o estão a usar," +
                        $"e torne novamente a apagá-lo";
                }

                return View("Error");
            }

        }

        public IActionResult ContractNotFound()
        {
            return View();
        }
    }
}
