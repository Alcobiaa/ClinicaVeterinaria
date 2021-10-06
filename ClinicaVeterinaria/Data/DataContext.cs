using ClinicaVeterinaria.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicaVeterinaria.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Vet> Vets { get; set; }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<VetAppointment> VetAppointments { get; set; }
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
