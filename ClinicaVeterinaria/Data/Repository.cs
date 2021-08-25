using ClinicaVeterinaria.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Vet> GetVets()
        {
            return _context.Vets.OrderBy(p => p.FirstName);
        }

        public Vet GetVet(int id)
        {
            return _context.Vets.Find(id);
        }

        public void AddVet(Vet vet)
        {
            _context.Vets.Add(vet);
        }

        public void UpdateVet(Vet vet)
        {
            _context.Vets.Update(vet);
        }

        public void RemoveVet(Vet vet)
        {
            _context.Vets.Remove(vet);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool VetExists(int id)
        {
            return _context.Vets.Any(p => p.Id == id);
        }
    }
}

