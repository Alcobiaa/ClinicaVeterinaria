﻿using ClinicaVeterinaria.Data.Entities;
using ClinicaVeterinaria.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
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

            var user = await _userHelper.GetUserByEmailAsync("lalobia62@gmail.com");

            if(user == null)
            {
                user = new User
                {
                    FirstName = "Goncalo",
                    LastName = "Alcobia",
                    Email = "lalobia62@gmail.com",
                    UserName = "lalobia62@gmail.com",
                    PhoneNumber = "919399412"
                };

                var result = await _userHelper.AddUserAsync(user, "123456");

                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }
        }
    }
}