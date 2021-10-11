using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaVeterinaria.Data.Entities
{
    public class AppointmentDetailTemp : IEntity
    {
        public int Id { get; set; }
        
        [Required]
        public Animal Animal { get; set; }

        [Required]
        public Vet Vet { get; set; }

        [Required]
        public int Room { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public User User { get; set; }
    }
}
