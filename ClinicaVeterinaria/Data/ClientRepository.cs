using ClinicaVeterinaria.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ClinicaVeterinaria.Data
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly DataContext _context;

        public ClientRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Clients.Include(c => c.User);
        }
    }
}
