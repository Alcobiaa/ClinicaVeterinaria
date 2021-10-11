using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Data.Entities;
using ClinicaVeterinaria.Helpers;
using ClinicaVeterinaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Controllers
{
    [Authorize(Roles = "Employee, Client")]
    public class VetAppointmentController : Controller
    {
        private readonly IVetAppointmentRepository _vetAppointment;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IVetRepository _vetRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly DataContext _context;

        public VetAppointmentController(IVetAppointmentRepository vetAppointment,
            IUserHelper userHelper,
            IConverterHelper converterHelper,
            IBlobHelper blobHelper,
            IVetRepository vetRepository,
            IAnimalRepository animalRepository,
            DataContext context)

        {
            _vetAppointment = vetAppointment;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
            _blobHelper = blobHelper;
            _vetRepository = vetRepository;
            _animalRepository = animalRepository;
            _context = context;
        }


        // GET: VetAppointmentController
        public async Task<ActionResult> Index()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            //var haveVetAppointments = _context.VetAppointments.FromSqlRaw($"SELECT * FROM dbo.VetAppointments WHERE UserId = {user.Id}").ToList();

            if (user.RoleName == "Employee")
            {
                return View(_vetAppointment.GetAll().OrderBy(v => v.Id));
            }

            if(user.RoleName == "Client")
            {
                return View(_context.VetAppointments.Where(v => v.ClientName == user.FirstName + " " + user.LastName));
            }

            return View();
        }

        public ActionResult NoVetAppointments()
        {
            return View();
        } 


        // GET: VetAppointmentController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VetAppointmentNotFound");
            }

            var vetAppointment = await _vetAppointment.GetByIdAsync(id.Value);

            if (vetAppointment == null)
            {
                return new NotFoundViewResult("VetAppointmentNotFound");
            }

            return View(vetAppointment);
        }

        // GET: VetAppointmentController/Create
        public ActionResult Create()
        {
            var model = new VetAppointmentViewModel
            {
                Animals = _vetAppointment.GetComboAnimals(),
                Vets = _vetAppointment.GetComboVets()
            };

            return View(model);
        }

        // POST: VetAppointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VetAppointmentViewModel model)
        {
            var vet = await _vetRepository.GetByIdAsync(model.VetId);
            var animal = await _animalRepository.GetByIdAsync(model.AnimalId);
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            model.VetName = vet.FirstName + " " + vet.LastName;
            model.AnimalName = animal.Name;
            model.ClientName = animal.ClientName;

            if (ModelState.IsValid)
            {
                //model.UserId = user.User
                await _vetAppointment.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: VetAppointmentController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VetAppointmentNotFound");
            }

            var vetAppointment = await _vetAppointment.GetByIdAsync(id.Value);

            var model = _converterHelper.ToVetAppointmentViewModel(vetAppointment);

            model.Vets = _vetAppointment.GetComboVets();
            model.Animals = _vetAppointment.GetComboAnimals();

            if (vetAppointment == null)
            {
                return new NotFoundViewResult("VetAppointmentNotFound");
            }

            return View(model);
        }

        // POST: VetAppointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VetAppointmentViewModel model)
        {
            var vet = await _vetRepository.GetByIdAsync(model.VetId);
            var animal = await _animalRepository.GetByIdAsync(model.AnimalId);
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            model.VetName = vet.FirstName + " " + vet.LastName;
            model.AnimalName = animal.Name;
            model.ClientName = animal.ClientName;

            if (ModelState.IsValid)
            {
                try
                {

                    var vetAppointment = _converterHelper.ToVetAppointment(model, false);

                    await _vetAppointment.UpdateAsync(vetAppointment);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _vetAppointment.ExistAsync(model.Id))
                    {
                        return new NotFoundViewResult("VetAppointmentNotFound");
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

        // GET: VetAppointmentController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VetAppointmentNotFound");
            }

            var vetAppointment = await _vetAppointment.GetByIdAsync(id.Value);

            if (vetAppointment == null)
            {
                return new NotFoundViewResult("VetAppointmentNotFound");
            }

            return View(vetAppointment);
        }

        // POST: VetAppointmentController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var vetAppointment = await _vetAppointment.GetByIdAsync(id);
            await _vetAppointment.DeleteAsync(vetAppointment);
            return RedirectToAction(nameof(Index));
        }
    }
}
