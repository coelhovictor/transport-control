using Core.Domain.Account;
using Core.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Core.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authentication;
        private readonly IConfiguration _configuration;

        public AccountController(IAuthenticate authentication, IConfiguration configuration)
        {
            _authentication = authentication;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _authentication.Authenticate(model.Email, model.Password);
            if(result)
            {
                if(string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(model.ReturnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> LoginTest()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            string email = "usuario@localhost";
            string password = env == "Development" ? _configuration.GetSection("AppSettings")["PasswordDefaultUsuario"] :
                Environment.GetEnvironmentVariable("PASSWORD_DEFAULT_USUARIO");

            var result = await _authentication.Authenticate(email, password);
            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
                return View("Login", new LoginViewModel());
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _authentication.RegisterUser(model.Email, model.Password, model.FirstName, 
                model.LastName, null, null, null);

            if(result.Success)
            {
                return Redirect("/");
            }
            else
            {
                ModelState.AddModelError(string.Empty, $"Invalid register attempt: {result.Message}");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authentication.Logout();
            return Redirect("/account/login");
        }
    }
}
