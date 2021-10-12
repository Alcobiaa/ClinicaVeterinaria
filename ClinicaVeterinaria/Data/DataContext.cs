using ClinicaVeterinaria.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ClinicaVeterinaria.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Vet> Vets { get; set; }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<VetAppointment> VetAppointments { get; set; }

        public DbSet<UsersClients> UsersClients { get; set; }

        public DbSet<History> Historys { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //Habilitar a regra de apagar em cascata(Cascade Delete Rule)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
