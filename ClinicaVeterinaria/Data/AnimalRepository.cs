using ClinicaVeterinaria.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Data
{
    public class AnimalRepository : GenericRepository<Animal>, IAnimalRepository
    {
        private readonly DataContext _context;

        public AnimalRepository(DataContext context) : base (context)
        {
            _context = context;
        }
        
        public IQueryable GetAllWithUsers()
        {
            return _context.Animals.Include(a => a.User);
        }
    }
}
