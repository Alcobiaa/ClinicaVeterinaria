using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Models
{
    public class SMSViewModel
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
