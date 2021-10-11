using ClinicaVeterinaria.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ClinicaVeterinaria.Models
{
    public class HistoryViewModel : History
    {
        public IEnumerable<SelectListItem> Animals { get; set; }

        public IEnumerable<SelectListItem> Vets { get; set; }
    }
}
