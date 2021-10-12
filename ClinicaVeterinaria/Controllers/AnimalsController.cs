using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Data.Entities;
using ClinicaVeterinaria.Helpers;
using ClinicaVeterinaria.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IClientRepository _clientRepository;
        private readonly DataContext _context;
        private readonly IUsersClientsRepository _usersClientsRepository;

        public AnimalsController(IAnimalRepository animalRepository,
            IUserHelper userHelper,
            IConverterHelper converterHelper,
            IBlobHelper blobHelper,
            IClientRepository clientRepository,
            DataContext context,
            IUsersClientsRepository usersClientsRepository)
        {
            _userHelper = userHelper;
            _animalRepository = animalRepository;
            _converterHelper = converterHelper;
            _blobHelper = blobHelper;
            _clientRepository = clientRepository;
            _context = context;
            _usersClientsRepository = usersClientsRepository;
        }

        // GET: Animals
        [Authorize(Roles = "Employee, Client")]
        public IActionResult Index()
        {
            return View(_animalRepository.GetAll().OrderBy(a => a.Name));
        }

        // GET: Animals/Details/5
        [Authorize(Roles = "Employee, Client")]
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
        [Authorize(Roles = "Employee")]
        public IActionResult Create()
        {
            var model = new AnimalViewModel
            {
                Clients = _animalRepository.GetComboClients()
            };

            return View(model);
        }

        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Create(AnimalViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "animals");
                }

                var userClient = await _usersClientsRepository.GetByIdAsync(model.UsersClientsId);
                var animal = _converterHelper.ToAnimal(model, imageId, true);

                model.ClientName = userClient.FirstName + " " + userClient.LastName;
                animal.ClientName = userClient.FirstName + " " + userClient.LastName;
                animal.UsersClientsId = userClient.Id;

                //TODO: Modificar para o user que tiver logado
                animal.User = await _userHelper.GetUserByEmailAsync("lalobia62@gmail.com");
                await _animalRepository.CreateAsync(animal);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Animals/Edit/5
        [Authorize(Roles = "Employee, Client")]
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

            model.Clients = _animalRepository.GetComboClients();

            return View(model);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee, Client")]
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

                    var userClient = await _usersClientsRepository.GetByIdAsync(model.UsersClientsId);
                    var animal = _converterHelper.ToAnimal(model, imageId, false);

                    model.ClientName = userClient.FirstName + " " + userClient.LastName;
                    animal.ClientName = userClient.FirstName + " " + userClient.LastName;
                    animal.UsersClientsId = userClient.Id;

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
        [Authorize(Roles = "Employee")]
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
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _animalRepository.GetByIdAsync(id);

            try
            {
                await _animalRepository.DeleteAsync(animal);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{animal.Name} probaly have an appointment";
                    ViewBag.ErrorMessage = $"{animal.Name} cannot be deleted";
                }

                return View("Error");
            }
        }

        public IActionResult AnimalNotFound()
        {
            return View();
        }
    }
}
