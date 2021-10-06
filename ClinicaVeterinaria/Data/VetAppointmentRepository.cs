using ClinicaVeterinaria.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ClinicaVeterinaria.Data
{
    public class VetAppointmentRepository : GenericRepository<VetAppointment>, IVetAppointmentRepository
    {
        private readonly DataContext _context;

        public VetAppointmentRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.VetAppointments.Include(v => v.User);
        }

        public IEnumerable<SelectListItem> GetComboAnimals()
        {
            var list = _context.Animals.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()

            }).OrderBy(l => l.Text).ToList();


            list.Insert(0, new SelectListItem
            {
                Text = "(Select a Animal...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboVets()
        {
            var list = _context.Vets.Select(c => new SelectListItem
            {
                Text = c.FirstName + " " + c.LastName,
                Value = c.Id.ToString()

            }).OrderBy(l => l.Text).ToList();


            list.Insert(0, new SelectListItem
            {
                Text = "(Select a Vet...)",
                Value = "0"
            });

            return list;
        }
    }
}
