using ClinicaVeterinaria.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);
    }
}
