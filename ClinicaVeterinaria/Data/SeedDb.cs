using ClinicaVeterinaria.Data.Entities;
using ClinicaVeterinaria.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;

        public SeedDb(DataContext context, 
            IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            // Verifica se já existe base de dados, caso nao exista cria
            await _context.Database.EnsureCreatedAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Client");
            await _userHelper.CheckRoleAsync("Employee");

            var user = await _userHelper.GetUserByEmailAsync("lalobia62@gmail.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Goncalo",
                    LastName = "Alcobia",
                    Email = "lalobia62@gmail.com",
                    UserName = "lalobia62@gmail.com",
                };

                var result = await _userHelper.AddUserAsync(user, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");

            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            if (!_context.Vets.Any())
            {
                AddVet("Tiago", "Pinto", user);
                AddVet("João", "Francisco", user);
                AddVet("Bruno", "Almeida", user);
                await _context.SaveChangesAsync();
            }
        }

        private void AddVet(string firstName, string lastName, User user)
        {
            _context.Vets.Add(new Vet
            {
                FirstName = firstName,
                LastName = lastName,
                User = user
            });
        }
    }
}
