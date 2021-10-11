using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Data.Entities
{
    public class Vet : IEntity
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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Age { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public User User { get; set; }

        //public string FullName => $"{FirstName} {LastName}"

        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://clinicaveterinariacet57.azurewebsites.net/images/noimage.png"
            : $"https://clinicaveterinaria.blob.core.windows.net/vets/{ImageId}";
    }
}
