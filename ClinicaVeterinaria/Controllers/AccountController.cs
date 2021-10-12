using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Data.Entities;
using ClinicaVeterinaria.Helpers;
using ClinicaVeterinaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly IMailHelper _mailHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IUsersClientsRepository _usersClientsRepository;
        private readonly DataContext _context;
        private readonly IVetAppointmentRepository _vetAppointmentRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly IHistoryRepository _historyRepository;

        public AccountController(IUserHelper userHelper,
            IConfiguration configuration,
            IMailHelper mailHelper,
            IBlobHelper blobHelper,
            IUsersClientsRepository usersClientsRepository,
            DataContext context,
            IVetAppointmentRepository vetAppointmentRepository,
            IAnimalRepository animalRepository,
            IHistoryRepository historyRepository)
        {
            _userHelper = userHelper;
            _configuration = configuration;
            _mailHelper = mailHelper;
            _blobHelper = blobHelper;
            _usersClientsRepository = usersClientsRepository;
            _context = context;
            _vetAppointmentRepository = vetAppointmentRepository;
            _animalRepository = animalRepository;
            _historyRepository = historyRepository;
        }

        public IActionResult Index()
        {
            return View(_userHelper.GetAll());
        }
        

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);

                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return RedirectToAction(this.Request.Query["ReturnUrl"].First());
                    }

                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to Login");
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            var model = new RegisterNewUserViewModel
            {
                Roles = _userHelper.GetComboRoles(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);

                var role = await _userHelper.GetRoleAsync(model.RoleID.ToString());

                if (user == null)
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Username,
                        UserName = model.Username,
                        RoleName = role.Name
                    };

                    var result = await _userHelper.AddUserAsync(user, model.Password);
                   
                    if (result != IdentityResult.Success)
                    {
                        ModelState.AddModelError(string.Empty, "The user couldn´t be created.");
                        return View(model);
                    }

                    await _userHelper.AddUserToRoleAsync(user, role.ToString());

                    var isInRole = await _userHelper.IsUserInRoleAsync(user, role.ToString());

                    if (!isInRole)
                    {
                        await _userHelper.AddUserToRoleAsync(user, role.ToString());
                    }

                    if (role.Name == "Client")
                    {
                        var userClient = new UsersClients
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = model.Username,
                            RoleName = role.Name
                        };

                        var result2 = _usersClientsRepository.CreateAsync(userClient);
                    }

                    string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);

                    var tokenLink = Url.Action("ConfirmationEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    
                    Response response = _mailHelper.SendEmail(model.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                       $"To allow the user, " +
                       $"plase click in this link to change your Password:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");


                    if (response.IsSuccess)
                    {
                        ViewBag.Message = "The instructions to allow you user has been sent to email";
                        return View(model);
                    }

                    ModelState.AddModelError(string.Empty, "The user couldn't be logged");
                }
            }

            return View(model);
        }


        [Authorize(Roles = "Admin, Employee, Client")]
        public async Task<IActionResult> ChangeUser()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            var model = new ChangeUserViewModel();

            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.ImageId = user.ImageId;
            }

            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "Admin, Employee, Client")]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                if (user != null)
                {
                    Guid imageId = model.ImageId;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                    }

                    model.ImageId = imageId;

                    
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.ImageId = model.ImageId;
                    
                    var response = await _userHelper.UpdateUserAsync(user);

                    if(response.Succeeded)
                    {
                        ViewBag.UserMessage = "User updated!";
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }
                }
            }
          
            return View(model);
        }

        
        [Authorize(Roles = "Admin, Employee, Client")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Employee, Client")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            return this.View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user != null)
                {
                    var result = await _userHelper.ValidatePasswordAsync(
                        user,
                        model.Password);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(15),
                            signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return this.Created(string.Empty, results);

                    }
                }
            }

            return BadRequest();
        }


        public IActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
                    return View(model);
                }

                var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

                var link = this.Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);

                Response response = _mailHelper.SendEmail(model.Email, "Vet Password Reset", $"<h1>Vet Password Reset</h1>" +
                $"To reset the password click in this link:</br></br>" +
                $"<a href = \"{link}\">Reset Password</a>");

                if (response.IsSuccess)
                {
                    this.ViewBag.Message = "The instructions to recover your password has been sent to email.";
                }

                return this.View();
            }

            return this.View(model);
        }


        public IActionResult ResetPassword(string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userHelper.GetUserByEmailAsync(model.Username);

            if (user != null)
            {
                var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    this.ViewBag.Message = "Password reset successful.";
                    return View();
                }

                this.ViewBag.Message = "Error while resetting the password.";
                return View(model);
            }

            this.ViewBag.Message = "User not found.";
            return View(model);
        }


        public async Task<IActionResult> ConfirmationEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {

            }

            //return RedirectToAction("Account", "FirstChangePassword");
            return View(nameof(FirstChangePassword));
        }


        public IActionResult FirstChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FirstChangePassword(ResetPasswordViewModel model)
        {
            var user = await _userHelper.GetUserByEmailAsync(model.Username);

            var Token = await _userHelper.GeneratePasswordResetTokenAsync(user);

            if (user != null)
            {
                var result = await _userHelper.ResetPasswordAsync(user, Token, model.Password);
                if (result.Succeeded)
                {
                    ViewBag.Message = "Password reset successful.";
                    return View();
                }

                ViewBag.Message = "Error while resetting the password.";
                return View(model);
            }

            ViewBag.Message = "User not found.";
            return View(model);
        }


        public IActionResult NotAuthorized()
        {
            return View();
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ClientNotFound");
            }

            var client = await _usersClientsRepository.GetByIdAsync(id.Value);

            if (client == null)
            {
                return new NotFoundViewResult("ClientNotFound");
            }

            return View(client);
        }


        public async Task<IActionResult> History()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            var history = _historyRepository.GetAll().Where(u => u.ClientName == user.FirstName + " " + user.LastName);
            
            return View(_historyRepository.GetAll().Where(u => u.ClientName == user.FirstName + " " + user.LastName));
        }

    }
}
