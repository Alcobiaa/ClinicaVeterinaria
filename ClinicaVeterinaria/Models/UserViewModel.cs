using ClinicaVeterinaria.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ClinicaVeterinaria.Models
{
    public class UserViewModel : User
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
