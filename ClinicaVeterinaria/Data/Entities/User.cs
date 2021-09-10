using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ClinicaVeterinaria.Data.Entities
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
    }
}
