using ClinicaVeterinaria.Data.Entities;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Helpers
{
    public interface IConverterHelper
    {
        Vet ToVet(VetViewModel model, string path, bool isNew);

        VetViewModel ToVetViewModel(Vet vet);

        Animal ToAnimal(AnimalViewModel model, string path, bool isNew);

        AnimalViewModel ToAnimalViewModel(Animal animal);

        Client ToClient(ClientViewModel model, string path, bool isNew);

        ClientViewModel ToClientViewModel(Client client);
    }
}
