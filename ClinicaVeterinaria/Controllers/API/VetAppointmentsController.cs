using ClinicaVeterinaria.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaVeterinaria.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class VetAppointmentsController : Controller
    {
        private readonly IVetAppointmentRepository _vetAppointmentRepository;

        public VetAppointmentsController(IVetAppointmentRepository vetAppointmentRepository)
        {
            _vetAppointmentRepository = vetAppointmentRepository;
        }

        [HttpGet]
        public IActionResult GetVetAppointments()
        {
            return Ok(_vetAppointmentRepository.GetAll());
        }
    }
}
