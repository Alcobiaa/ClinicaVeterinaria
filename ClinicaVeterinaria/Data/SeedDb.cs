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

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            // Verifica se já existe base de dados, caso nao exista cria
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Vets.Any())
            {
                AddVet("Tiago", "Pinto");
                AddVet("João", "Francisco");
                AddVet("Bruno", "Almeida");
                await _context.SaveChangesAsync();
            }

            var user = await _userHelper.GetUserByEmailAsync("goncalo@yopmail.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Goncalo",
                    LastName = "Alcobia",
                    Email = "goncalo@yopmail.com",
                    UserName = "goncalo@yopmail.com",
                };

                var result = await _userHelper.AddUserAsync(user, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }
        }

        private void AddVet(string firstName, string lastName)
        {
            _context.Vets.Add(new Vet
            {
                FirstName = firstName,
                LastName = lastName,
            });


        }
    }
}
