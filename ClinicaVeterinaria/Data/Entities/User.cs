using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaVeterinaria.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        public string RoleName { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://clinicaveterinariatpsi.azurewebsites.net/images/noimage.png"
            : $"https://clinicaveterinariatpsi.blob.core.windows.net/users/{ImageId}";
    }
}
