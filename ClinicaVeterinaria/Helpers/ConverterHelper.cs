using ClinicaVeterinaria.Data.Entities;
using ClinicaVeterinaria.Models;
using System;

namespace ClinicaVeterinaria.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Vet ToVet(VetViewModel model, Guid imageId, bool isNew)
        {
            return new Vet
            {
                Id = isNew ? 0 : model.Id,
                ImageId = imageId,
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
                ImageId = vet.ImageId,
                PhoneNumber = vet.PhoneNumber,
                User = vet.User
            };
        }

        public Animal ToAnimal(AnimalViewModel model, Guid imageId, bool isNew)
        {
            return new Animal
            {
                Id = isNew ? 0 : model.Id,
                ImageId = imageId,
                Name = model.Name,
                Age = model.Age,
                Weight = model.Weight,
                Breeds = model.Breeds,
                Species = model.Species,
                User = model.User,
                ClientName = model.ClientName,
            };
        }

        public AnimalViewModel ToAnimalViewModel(Animal animal)
        {
            return new AnimalViewModel
            {
                Id = animal.Id,
                ImageId = animal.ImageId,
                Name = animal.Name,
                Age = animal.Age,
                Weight = animal.Weight,
                Breeds = animal.Breeds,
                Species = animal.Species,
                ClientName = animal.ClientName,
                User = animal.User,
            };
        }

        public Client ToClient(ClientViewModel model, Guid imageId, bool isNew)
        {
            return new Client
            {
                Id = isNew ? 0 : model.Id,
                ImageId = imageId,
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
                ImageId = client.ImageId,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                User = client.User,
            };
        }

        public VetAppointment ToVetAppointment(VetAppointmentViewModel model, bool isNew)
        {
            return new VetAppointment
            {
                Id = isNew ? 0 : model.Id,
                AnimalId = model.AnimalId,
                AnimalName = model.AnimalName,
                VetId = model.VetId,
                VetName = model.VetName,
                Room = model.Room,
                Date = model.Date,
                ClientName = model.ClientName
            };
        }

        public VetAppointmentViewModel ToVetAppointmentViewModel(VetAppointment vetAppointment)
        {
            return new VetAppointmentViewModel
            {
                Id = vetAppointment.Id,
                AnimalId = vetAppointment.AnimalId,
                AnimalName = vetAppointment.AnimalName,
                VetId = vetAppointment.VetId,
                VetName = vetAppointment.VetName,
                Room = vetAppointment.Room,
                Date = vetAppointment.Date,
                ClientName = vetAppointment.ClientName
            };
        }
    }
}
