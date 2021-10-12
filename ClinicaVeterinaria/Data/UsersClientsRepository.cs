using ClinicaVeterinaria.Data.Entities;
using ClinicaVeterinaria.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Data
{
    public class UsersClientsRepository : GenericRepository<UsersClients>, IUsersClientsRepository
    {
        private readonly DataContext _context;

        public UsersClientsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public Task GetByIdStringAsync(string idString)
        {
            return _context.UsersClients
                .AsNoTracking().
                FirstOrDefaultAsync(e => e.IdString == idString);
        }
    }
}
