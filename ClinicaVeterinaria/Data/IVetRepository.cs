using ClinicaVeterinaria.Data.Entities;
using System.Linq;

namespace ClinicaVeterinaria.Data
{
    public interface IVetRepository : IGenericRepository<Vet>
    {
        public IQueryable GetAllWithUsers();
    }
}
