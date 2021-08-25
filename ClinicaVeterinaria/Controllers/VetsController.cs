using System.Threading.Tasks;
using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ClinicaVeterinaria.Controllers
{
    public class VetsController : Controller
    {
        private readonly IRepository _repository;

        public VetsController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: Vets
        public IActionResult Index()
        {
            return View(_repository.GetVets());
        }

        // GET: Vets/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vet = _repository.GetVet(id.Value);

            if (vet == null)
            {
                return NotFound();
            }

            return View(vet);
        }

        // GET: Vets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vet vet)
        {
            if (ModelState.IsValid)
            {
                _repository.AddVet(vet);
                await _repository.SaveAllAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vet);
        }

        // GET: Vets/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vet = _repository.GetVet(id.Value);

            if (vet == null)
            {
                return NotFound();
            }
            return View(vet);
        }

        // POST: Vets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vet vet)
        {
            if (id != vet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdateVet(vet);
                    await _repository.SaveAllAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repository.VetExists(vet.Id))
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
            return View(vet);
        }

        // GET: Vets/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vet = _repository.GetVet(id.Value);

            if (vet == null)
            {
                return NotFound();
            }

            return View(vet);
        }

        // POST: Vets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vet = _repository.GetVet(id);
            _repository.RemoveVet(vet);
            await _repository.SaveAllAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
