using ClinicaVeterinaria.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Data
{
    public class VetRepository : GenericRepository<Vet>, IVetRepository
    {
        private readonly DataContext _context;

        public VetRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
