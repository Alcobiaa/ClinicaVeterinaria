using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaVeterinaria.Data.Entities
{
    public class VetAppointment : IEntity
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

        public int Room { get; set; }

        public DateTime Date { get; set; }

        public DateTime Hour { get; set; }

        public User User { get; set; }
    }
}
