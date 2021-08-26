using ClinicaVeterinaria.Data.Entities;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Vet ToVet(VetViewModel model, string path, bool isNew)
        {
            return new Vet
            {
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Age = model.Age,
                User = model.User
            };
        }

        public VetViewModel ToVetViewModel(Vet vet)
        {
            return new VetViewModel
            {
                Id = vet.Id,
                FirstName = vet.FirstName,
                LastName = vet.LastName,
                Age = vet.Age,
                Email = vet.Email,
                ImageUrl = vet.ImageUrl,
                PhoneNumber = vet.PhoneNumber,
                User = vet.User
            };
        }
    }
}
