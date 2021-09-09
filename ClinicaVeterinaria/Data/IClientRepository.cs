using ClinicaVeterinaria.Data.Entities;
using System.Linq;

namespace ClinicaVeterinaria.Data
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        public IQueryable GetAllWithUsers();
    }
}
