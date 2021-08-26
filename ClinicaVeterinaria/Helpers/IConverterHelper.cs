using ClinicaVeterinaria.Data.Entities;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Helpers
{
    public interface IConverterHelper
    {
        Vet ToVet(VetViewModel model, string path, bool isNew);

        VetViewModel ToVetViewModel(Vet vet);
    }
}
