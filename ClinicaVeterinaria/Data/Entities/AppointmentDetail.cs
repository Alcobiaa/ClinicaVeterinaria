using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Data.Entities
{
    public class AppointmentDetail: IEntity
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
    }
}
