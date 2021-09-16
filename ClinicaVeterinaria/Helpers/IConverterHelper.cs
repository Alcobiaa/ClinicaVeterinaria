using ClinicaVeterinaria.Data.Entities;
using ClinicaVeterinaria.Models;
using System;

namespace ClinicaVeterinaria.Helpers
{
    public interface IConverterHelper
    {
        Vet ToVet(VetViewModel model, Guid imageId, bool isNew);

        VetViewModel ToVetViewModel(Vet vet);

        Animal ToAnimal(AnimalViewModel model, Guid imageId, bool isNew);

        AnimalViewModel ToAnimalViewModel(Animal animal);

        Client ToClient(ClientViewModel model, Guid imageId, bool isNew);

        ClientViewModel ToClientViewModel(Client client);
    }
}
