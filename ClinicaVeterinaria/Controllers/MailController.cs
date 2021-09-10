using ClinicaVeterinaria.Helpers;
using ClinicaVeterinaria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Controllers
{
    public class MailController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;

        public MailController(IUserHelper userHelper,
            IMailHelper mailHelper)
        {
            _userHelper = userHelper;
            _mailHelper = mailHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(MailViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "The email doesn´t correspont to registered user.");
                    return View(model);
                }

                Response response = _mailHelper.SendEmail(model.Email, model.Subject, model.Message);

                if (response.IsSuccess)
                {
                    this.ViewBag.Message = "The e-mail has been sent...";
                }

                return this.View();
            }

            return this.View(model);
        }
    }
}
