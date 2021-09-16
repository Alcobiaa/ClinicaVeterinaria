using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Helpers;
using ClinicaVeterinaria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IAnimalRepository _animalRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IBlobHelper _blobHelper;

        public AnimalsController(IAnimalRepository animalRepository,
            IUserHelper userHelper,
            IConverterHelper converterHelper,
            IBlobHelper blobHelper)
        {
            _userHelper = userHelper;
            _animalRepository = animalRepository;
            _converterHelper = converterHelper;
            _blobHelper = blobHelper;
        }

        // GET: Animals
        public IActionResult Index()
        {
            return View(_animalRepository.GetAll().OrderBy(a => a.Name));
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                //TODO: Fazer a view do Not found
                return new NotFoundViewResult("AnimalNotFound");
            }

            var animal = await _animalRepository.GetByIdAsync(id.Value);

            if (animal == null)
            {
                //TODO: Fazer a view do Not found
                return new NotFoundViewResult("AnimalNotFound");
            }

            return View(animal);
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimalViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "animals");
                }

                var animal = _converterHelper.ToAnimal(model, imageId, true);

                //TODO: Modificar para o user que tiver logado
                animal.User = await _userHelper.GetUserByEmailAsync("lalobia62@gmail.com");
                await _animalRepository.CreateAsync(animal);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //TODO: Fazer a view do Not found
                return new NotFoundViewResult("AnimalNotFound");
            }

            var animal = await _animalRepository.GetByIdAsync(id.Value);

            if (animal == null)
            {
                //TODO: Fazer a view do Not found
                return new NotFoundViewResult("AnimalNotFound");
            }

            var model = _converterHelper.ToAnimalViewModel(animal);

            return View(model);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AnimalViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Guid imageId = model.ImageId;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "animals");
                    }

                    var animal = _converterHelper.ToAnimal(model, imageId, false);

                    //TODO: Modificar para o user que estiver logado
                    animal.User = await _userHelper.GetUserByEmailAsync("lalobia62@gmail.com");
                    await _animalRepository.UpdateAsync(animal);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _animalRepository.ExistAsync(model.Id))
                    {
                        //TODO: Fazer a view do Not found
                        return new NotFoundViewResult("AnimalNotFound");
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

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                //TODO: Fazer a view do Not found
                return new NotFoundViewResult("AnimalNotFound");
            }

            var animal = await _animalRepository.GetByIdAsync(id.Value);

            if (animal == null)
            {
                //TODO: Fazer a view do Not found
                return new NotFoundViewResult("AnimalNotFound");
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _animalRepository.GetByIdAsync(id);
            await _animalRepository.DeleteAsync(animal);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AnimalNotFound()
        {
            return View();
        }
    }
}
