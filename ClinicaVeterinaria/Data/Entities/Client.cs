using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaVeterinaria.Data.Entities
{
    public class Client : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public User User { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://clinicaveterinariatpsi.azurewebsites.net/images/noimage.png"
            : $"https://clinicaveterinariatpsi.blob.core.windows.net/clients/{ImageId}";
    }
}
