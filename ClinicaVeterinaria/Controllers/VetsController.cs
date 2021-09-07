using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Helpers;
using ClinicaVeterinaria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace ClinicaVeterinaria.Controllers
{
    public class VetsController : Controller
    {
        private readonly IVetRepository _vetRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public VetsController(IVetRepository vetRepository,
            IUserHelper userHelper,
            IImageHelper imageHelper,
            IConverterHelper converterHelper)
        {
            _vetRepository = vetRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        // GET: Vets
        public IActionResult Index()
        {
            return View(_vetRepository.GetAll().OrderBy(p => p.FirstName));
        }

        // GET: Vets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VetNotFound");
            }

            var vet = await _vetRepository.GetByIdAsync(id.Value);

            if (vet == null)
            {
                return new NotFoundViewResult("VetNotFound");
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
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "vets");
                }

                var vet = _converterHelper.ToVet(model, path, true);

                //TODO: Modificar para o user que tiver logado
                vet.User = await _userHelper.GetUserByEmailAsync("lalobia62@gmail.com");
                await _vetRepository.CreateAsync(vet);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Vets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VetNotFound");
            }

            var vet = await _vetRepository.GetByIdAsync(id.Value);

            if (vet == null)
            {
                return new NotFoundViewResult("VetNotFound");
            }

            var model = _converterHelper.ToVetViewModel(vet);

            return View(model);
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

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "vets");
                    }

                    var vet = _converterHelper.ToVet(model, path, false);

                    //TODO: Modificar para o user que estiver logado
                    vet.User = await _userHelper.GetUserByEmailAsync("lalobia62@gmail.com");
                    await _vetRepository.UpdateAsync(vet);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _vetRepository.ExistAsync(model.Id))
                    {
                        return new NotFoundViewResult("VetNotFound");
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
                return new NotFoundViewResult("VetNotFound");
            }

            var vet = await _vetRepository.GetByIdAsync(id.Value);

            if (vet == null)
            {
                return new NotFoundViewResult("VetNotFound");
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

        public IActionResult VetNotFound()
        {
            return View();
        }

    }
}
