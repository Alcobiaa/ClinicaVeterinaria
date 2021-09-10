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

        public Animal ToAnimal(AnimalViewModel model, string path, bool isNew)
        {
            return new Animal
            {
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                Name = model.Name,
                Age = model.Age,
                Weight = model.Weight,
                Breeds = model.Breeds,
                Species = model.Species,
                User = model.User,
                Owner = model.Owner
            };
        }

        public AnimalViewModel ToAnimalViewModel(Animal animal)
        {
            return new AnimalViewModel
            {
               Id = animal.Id,
               ImageUrl = animal.ImageUrl,
               Name = animal.Name,
               Age = animal.Age,
               Weight = animal.Weight,
               Breeds = animal.Breeds,
               Species = animal.Species,
               User = animal.User,
               Owner = animal.Owner
            };
        }

        public Client ToClient(ClientViewModel model, string path, bool isNew)
        {
            return new Client
            {
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                User = model.User,
            };
        }

        public ClientViewModel ToClientViewModel(Client client)
        {
            return new ClientViewModel
            {
                Id = client.Id,
                ImageUrl = client.ImageUrl,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                User = client.User,
            };
        }

        public User ToUser(UserViewModel model, string path, bool isNew)
        {
            return new User
            {
                Id = isNew ? 0 : model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImageUrl = model.ImageUrl,
                UserName = model.UserName,
                Email = model.Email,
            };
        }

    }
}
