using Inventory.Application.Interfaces.IServices.IAuthService;
using Inventory.Application.ViewModels.User;
using Inventory.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = await _authService.RegisterAsync(model);
            if (result.Success)
            {
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", result.Message);
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _authService.LoginAsync(model);

            if (result.Success)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", result.Message);
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login");
        }
    }
}