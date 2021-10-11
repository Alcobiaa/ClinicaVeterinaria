using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Data.Entities
{
    public class History : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Animal Name")]
        public int AnimalId { get; set; }

        [Display(Name = "Animal Name")]
        public string AnimalName { get; set; }

        [Display(Name = "Vet Name")]
        public int VetId { get; set; }

        [Display(Name = "Vet Name")]
        public string VetName { get; set; }

        [Required]
        public int Room { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        public string ClientName { get; set; }
    }
}
