using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public DateTime Age { get; set; }

        [Required]
        public string Species { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        public string Breeds { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public string Owner { get; set; }

        public User User { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImageUrl))
                {
                    return null;
                }

                return $"https://clinicaveterinariacet57.azurewebsites.net{ImageUrl.Substring(1)}";
            }
        }
    }
}
