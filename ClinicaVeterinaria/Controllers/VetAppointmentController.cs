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
        private readonly IHistoryRepository _historyRepository;

        public VetAppointmentController(IVetAppointmentRepository vetAppointment,
            IUserHelper userHelper,
            IConverterHelper converterHelper,
            IBlobHelper blobHelper,
            IVetRepository vetRepository,
            IAnimalRepository animalRepository,
            DataContext context,
            IHistoryRepository historyRepository)

        {
            _vetAppointment = vetAppointment;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
            _blobHelper = blobHelper;
            _vetRepository = vetRepository;
            _animalRepository = animalRepository;
            _context = context;
            _historyRepository = historyRepository;
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

            if (user.RoleName == "Client")
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
            try
            {
                var vet = await _vetRepository.GetByIdAsync(model.VetId);
                var animal = await _animalRepository.GetByIdAsync(model.AnimalId);
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                if(vet == null && animal == null)
                {
                    ViewBag.Message = "Yoou have to select a option !";
                }
                else
                {
                    model.VetName = vet.FirstName + " " + vet.LastName;
                    model.AnimalName = animal.Name;
                    model.ClientName = animal.ClientName;

                    var model2 = new HistoryViewModel
                    {
                        VetName = vet.FirstName + " " + vet.LastName,
                        AnimalName = animal.Name,
                        ClientName = animal.ClientName,
                        Room = model.Room,
                        AnimalId = model.AnimalId,
                        VetId = model.VetId,
                        Date = model.Date,
                    };

                    bool verifica = false;

                    foreach (VetAppointment appointment in _vetAppointment.GetAll())
                    {
                        if (model.VetName == appointment.VetName && model.Date == appointment.Date)
                        {
                            ViewBag.Message = "You cannot make this appointment, choose a different date or another Animal";
                            verifica = true;
                        }
                        else if(model.AnimalName == appointment.AnimalName && model.Date == appointment.Date)
                        {
                            ViewBag.Message = "You cannot make this appointment, choose a different date or another Animal";
                            verifica = true;
                        }
                        else if(model.Date == appointment.Date && model.Room == appointment.Room)
                        {
                            ViewBag.Message = "You cannot make this appointment, choose a different Room";
                            verifica = true;
                        }
                    }


                    if(verifica == false)
                    {
                        //model.UserId = user.User
                        await _vetAppointment.CreateAsync(model);
                        await _historyRepository.CreateAsync(model2);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        model.Animals = _vetAppointment.GetComboAnimals();
                        model.Vets = _vetAppointment.GetComboVets();
                    }

                    //if (ModelState.IsValid)
                    //{
                    //    //model.UserId = user.User
                    //    await _vetAppointment.CreateAsync(model);
                    //    await _historyRepository.CreateAsync(model2);
                    //    return RedirectToAction(nameof(Index));
                    //}
                }

                //model.VetName = vet.FirstName + " " + vet.LastName;
                //model.AnimalName = animal.Name;
                //model.ClientName = animal.ClientName;

                //var model2 = new HistoryViewModel
                //{
                //    VetName = vet.FirstName + " " + vet.LastName,
                //    AnimalName = animal.Name,
                //    ClientName = animal.ClientName,
                //    Room = model.Room,
                //    AnimalId = model.AnimalId,
                //    VetId = model.VetId,
                //    Date = model.Date,
                //};

                //if (ModelState.IsValid)
                //{
                //    //model.UserId = user.User
                //    await _vetAppointment.CreateAsync(model);
                //    await _historyRepository.CreateAsync(model2);
                //    return RedirectToAction(nameof(Index));
                //}
            }
            catch (System.Exception)
            {
                ViewBag.Message = "Tem que selecionar alguma informação";
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

            try
            {
                bool verifica = false;

                foreach (VetAppointment appointment in _vetAppointment.GetAll())
                {
                    if (model.VetName == appointment.VetName && model.Date == appointment.Date)
                    {
                        ViewBag.Message = "You cannot make this appointment, choose a different date or another Animal";
                        verifica = true;
                    }
                    else if (model.AnimalName == appointment.AnimalName && model.Date == appointment.Date)
                    {
                        ViewBag.Message = "You cannot make this appointment, choose a different date or another Animal";
                        verifica = true;
                    }
                    else if (model.Date == appointment.Date && model.Room == appointment.Room)
                    {
                        ViewBag.Message = "You cannot make this appointment, choose a different Room";
                        verifica = true;
                    }
                }

                if (verifica == false)
                {
                    var vetAppointment = _converterHelper.ToVetAppointment(model, false);

                    await _vetAppointment.UpdateAsync(vetAppointment);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    model.Animals = _vetAppointment.GetComboAnimals();
                    model.Vets = _vetAppointment.GetComboVets();
                }
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

            return View(model);
        }


        // GET: VetAppointmentController/Delete/5
        [Authorize(Roles = "Employee")]
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
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var vetAppointment = await _vetAppointment.GetByIdAsync(id);
            await _vetAppointment.DeleteAsync(vetAppointment);
            return RedirectToAction(nameof(Index));
        }
    }
}
