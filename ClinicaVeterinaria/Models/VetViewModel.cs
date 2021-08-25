using ClinicaVeterinaria.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ClinicaVeterinaria.Models
{
    public class VetViewModel : Vet
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
