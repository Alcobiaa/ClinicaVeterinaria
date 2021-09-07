using ClinicaVeterinaria.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ClinicaVeterinaria.Models
{
    public class AnimalViewModel : Animal
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
