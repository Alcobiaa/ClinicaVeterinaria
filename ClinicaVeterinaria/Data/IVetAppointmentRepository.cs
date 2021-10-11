using ClinicaVeterinaria.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Data
{
    public interface IVetAppointmentRepository : IGenericRepository<VetAppointment>
    {
        //public IQueryable GetAllWithUsers();

        //Task<IQueryable<VetAppointment>> GetVetAppointmentAsync(string userName);

        IEnumerable<SelectListItem> GetComboAnimals();

        IEnumerable<SelectListItem> GetComboVets();
    }
}
