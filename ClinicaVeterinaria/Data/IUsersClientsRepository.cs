using ClinicaVeterinaria.Data.Entities;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Data
{
    public interface IUsersClientsRepository : IGenericRepository<UsersClients>
    {
        Task GetByIdStringAsync(string idString);
    }
}
