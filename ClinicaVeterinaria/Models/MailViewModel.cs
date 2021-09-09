using System.ComponentModel.DataAnnotations;

namespace ClinicaVeterinaria.Models
{
    public class MailViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
