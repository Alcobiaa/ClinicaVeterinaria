using ClinicaVeterinaria.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ClinicaVeterinaria.Data
{
    public interface IVetAppointmentRepository : IGenericRepository<VetAppointment>
    {
        public IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboAnimals();

        IEnumerable<SelectListItem> GetComboVets();
    }
}
