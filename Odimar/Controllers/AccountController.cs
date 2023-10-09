using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Odimar.Data;
using Odimar.Data.Entities;
using Odimar.Helpers;
using Odimar.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Odimar.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly IMailHelper _mailHelper;
        private readonly IDistrictRepository _districtRepository;

        public AccountController(IUserHelper userHelper, IDistrictRepository districtRepository, IConfiguration configuration, IMailHelper mailHelper)
        {
            _configuration = configuration;
            _mailHelper = mailHelper;
            _userHelper = userHelper;
            _districtRepository = districtRepository;
        }

        #region UserFunctions

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
            var user = await _userHelper.GetUserByEmailAsync(model.Username);
            if (user == null)
            {
                this.ModelState.AddModelError(string.Empty, "Error: User is non-existent");
                return View(model);
            }

            if (!user.AdminApproved)
            {
                this.ModelState.AddModelError(string.Empty, "Error: Account awaitting approval");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var result = await _userHelper.LoginAsync(model);
                    if (result.Succeeded)
                    {
                        if (this.Request.Query.Keys.Contains("ReturnUrl"))
                        {
                            return Redirect(this.Request.Query["ReturnUrl"].First());
                        }
                        return this.RedirectToAction("Index", "Home");
                    }
                }
                this.ModelState.AddModelError(string.Empty, "Error: Failed to Login");
            }
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
                Districts = _districtRepository.GetComboDistricts(),
                Counties = _districtRepository.GetComboCounties(0),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user == null)
                {
                    var county = await _districtRepository.GetCountyWithParishAsync(model.CountyId);
                    var parish = await _districtRepository.GetParishAsync(model.CountyId);
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Username,
                        UserName = model.Username,
                        Address = model.Address,
                        PhoneNumber = model.PhoneNumber,
                        CountyId = county.Id,
                        County = county,
                        ParishId = parish.Id,
                        Parish = parish,
                        EmployeeApproved = false,
                        AdminApproved = false
                    };

                    var result = await _userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        ModelState.AddModelError(string.Empty, "The user could not be created");
                        return View(model);
                    }

                    string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                    string tokenLink = Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken,

                    }, protocol: HttpContext.Request.Scheme);
                    await _userHelper.AddUserToRoleAsync(user, "Customer");

                    Response response = _mailHelper.SendEmail(model.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                        $"You've Registered for Odimar, An employee will comfirm your data after you confirm registration and then pass on to an admin to accept the information.<br></br>" +
                        $"You will recieve an email when the comfirmation process has been completed.<br></br><br></br>" +
                        $"To Complete registration, please click in the following link:</br></br><a href = \"{tokenLink}\">Confirm Registration</a>");

                    if (response.IsSucess)
                    {
                        ViewBag.Message = "Register Successfull, Approval pending... please verify your inbox for more information";
                        return View(model);
                    }

                    ModelState.AddModelError(string.Empty, "The user could not be logged");
                }
                model.Counties = _districtRepository.GetComboCounties(model.DistrictId);
                model.Districts = _districtRepository.GetComboDistricts();
            }
            return View(model);
        }

        public async Task<IActionResult> ChangeUser()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = new ChangeUserViewModel();
            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Address = user.Address;
                model.PhoneNumber = user.PhoneNumber;
                var parish = await _districtRepository.GetParishAsync(user.ParishId);
                if (parish != null)
                {
                    var county = await _districtRepository.GetCountyWithParishAsync(user.CountyId);
                    if (county != null)
                    {
                        var district = await _districtRepository.GetDistrictAsync(county);
                        if (district != null)
                        {
                            model.DistrictId = district.Id;
                            model.Counties = _districtRepository.GetComboCounties(district.Id);
                            model.Parishes = _districtRepository.GetComboParishes(district.Id,county.Id);

                            model.Districts = _districtRepository.GetComboDistricts();
                            model.CountyId = user.CountyId;
                            model.ParishId = user.ParishId;
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var county = await _districtRepository.GetCountyWithParishAsync(model.CountyId);
                    var parish = await _districtRepository.GetParishAsync(model.ParishId);

                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Address = model.Address;
                    user.PhoneNumber = model.PhoneNumber;
                    user.CountyId = model.CountyId;
                    user.County = county;
                    user.ParishId = model.ParishId;
                    user.Parish = parish;

                    var response = await _userHelper.UpdateUserAsync(user);
                    if (response.Succeeded)
                    {
                        ViewBag.UserMessage = "User Updated";
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }
                }
            }
            return View(model);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
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
                        ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User not found.");
                }
            }
            return this.View(model);
        }
        #endregion
        
        #region BackendFunctions
        public IActionResult NotAuthorized()
        {
            return View();
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

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }
            var user = await _userHelper.GetUserByIdAsync(userId);
            if (user == null || !user.AdminApproved || !user.EmployeeApproved)
            {
                return NotFound();
            }
            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return NotFound();
            }
            return View();
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
                    ModelState.AddModelError(string.Empty, "The email doesn't correspond to a registered user.");
                    return View(model);
                }

                var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

                var link = this.Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);

                Response response = _mailHelper.SendEmail(model.Email, "Odimar Password Reset", $"<h1>Odimar Password Reset</h1>" +
                $"To reset the password click the following link:</br></br>" +
                $"<a href = \"{link}\">Reset Password</a>");

                if (response.IsSucess)
                {
                    this.ViewBag.Message = "The instructions to recover your password has been sent to your email.";
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

        [HttpPost]
        [Route("Account/GetCountiesAsync")]
        public async Task<JsonResult> GetCountiesAsync(int districtId)
        {
            var district = await _districtRepository.GetDistrictWithCountyAsync(districtId);
            return Json(district.Counties.OrderBy(c => c.Name));
        }
        #endregion

        #region CRUDFunctions
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Create(string? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("AccountNotFound");
            }
            var user = await _userHelper.GetUserByIdAsync(id);

            await _userHelper.UpdateUserAsync(user);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("AccountNotFound");
            }

            var account = await _userHelper.GetUserByIdAsync(id);
            if (account == null)
            {
                return new NotFoundViewResult("AccountNotFound");
            }

            return View(account);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            try
            {
                //throw new Exception("Excepção de Teste");
                //await _userHelper.DeleteUserAsync(user);
                return RedirectToAction("Dashboard", "Home");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{user.FullName} provavelmente está a ser usado!!";
                    ViewBag.ErrorMessage = $"{user.FullName} não pode ser apagado visto a haverem contadores que o usam. </br></br>" +
                        $"Experimente primeiro apagar todos os contadores que o estão a usar," +
                        $"e torne novamente a apagá-lo";
                }

                return View("Error");
            }
        }
        #endregion
    }
}
