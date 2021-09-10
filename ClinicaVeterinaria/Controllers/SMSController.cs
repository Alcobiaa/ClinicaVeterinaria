using ClinicaVeterinaria.Helpers;
using ClinicaVeterinaria.Models;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ClinicaVeterinaria.Controllers
{
    public class SMSController : Controller
    {
        private readonly ISMSHelper _smsHelper;

        public SMSController(ISMSHelper smsHelper)
        {
            _smsHelper = smsHelper;
        }

        public ActionResult SendSMS()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendSMS(SMSViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                Response response = _smsHelper.SendSMS(model.PhoneNumber, model.Message);

                if (response.IsSuccess)
                {
                    this.ViewBag.Message = "The message has been sent...";
                }

                return this.View();
            }

            return View(model);
        }
    }
}
