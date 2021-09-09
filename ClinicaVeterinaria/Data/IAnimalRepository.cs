﻿using ClinicaVeterinaria.Data.Entities;
using System.Linq;

namespace ClinicaVeterinaria.Data
{
    public interface IAnimalRepository : IGenericRepository<Animal>
    {
        public IQueryable GetAllWithUsers();
    }
}
