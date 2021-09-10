using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Helpers
{
    public interface ISMSHelper
    {
        Response SendSMS(string to, string body);
    }
}
