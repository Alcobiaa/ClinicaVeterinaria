using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaVeterinaria.Data.Entities
{
    public class Animal : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string Age { get; set; }

        [Required]
        public string Species { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        public string Breeds { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        public int UsersClientsId { get; set; }

        public string ClientName { get; set; }

        public string UserID { get; set; }

        public User User { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://clinicaveterinariacet57.azurewebsites.net/images/noimage.png"
            : $"https://clinicaveterinaria.blob.core.windows.net/animals/{ImageId}";
    }
}
