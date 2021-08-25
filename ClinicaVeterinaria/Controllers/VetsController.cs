using System.IO;
using System.Threading.Tasks;
using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Data.Entities;
using ClinicaVeterinaria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ClinicaVeterinaria.Controllers
{
    public class VetsController : Controller
    {
        private readonly IVetRepository _vetRepository;

        public VetsController(IVetRepository vetRepository)
        {
            _vetRepository = vetRepository;
        }

        // GET: Vets
        public IActionResult Index()
        {
            return View(_vetRepository.GetAll());
        }

        // GET: Vets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vet = await _vetRepository.GetByIdAsync(id.Value);

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
        public async Task<IActionResult> Create(VetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\vet",
                        model.ImageFile.Name);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/vet/{model.ImageFile.FileName}";
                }

                var vet = this.ToVet(model, path);

                await _vetRepository.CreateAsync(vet);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private Vet ToVet(VetViewModel model, string path)
        {
            return new Vet
            {
                Id = model.Id,
                ImageUrl = path,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Age = model.Age
            };
        }

        // GET: Vets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vet = await _vetRepository.GetByIdAsync(id.Value);

            if (vet == null)
            {
                return NotFound();
            }

            var model = this.ToVetViewModel(vet);

            return View(model);
        }

        private VetViewModel ToVetViewModel(Vet vet)
        {
            return new VetViewModel
            {
                Id = vet.Id,
                FirstName = vet.FirstName,
                LastName = vet.LastName,
                Age = vet.Age,
                Email = vet.Email,
                ImageUrl = vet.ImageUrl,
                PhoneNumber = vet.PhoneNumber
            };
        }

        // POST: Vets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VetViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = model.ImageUrl;

                    if(model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        path = Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot\\images\\vet",
                            model.ImageFile.Name);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(stream);
                        }

                        path = $"~/images/vet/{model.ImageFile.FileName}";
                    }

                    var vet = this.ToVet(model, path);

                    //TODO: Modificar para o user que estiver logado
                    await _vetRepository.UpdateAsync(vet);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _vetRepository.ExistAsync(model.Id))
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
            return View(model);
        }

        // GET: Vets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vet = await _vetRepository.GetByIdAsync(id.Value);

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
            var vet = await _vetRepository.GetByIdAsync(id);
            await _vetRepository.DeleteAsync(vet);
            return RedirectToAction(nameof(Index));
        }

    }
}
