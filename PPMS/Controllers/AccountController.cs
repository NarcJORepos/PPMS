using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Helpers;

namespace PPMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            // Prevent already logged-in users from seeing login page
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserDTO loginDto)
        {
            //For testing password hashing
            //string xx = Helpers.PasswordHelper.HashPassword("111111111");

            if (!ModelState.IsValid) return View(loginDto);

            var authResult = await _authService.AuthenticateAsync(loginDto);

            if (authResult == null || !authResult.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(loginDto);
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, authResult.UserId.ToString()),
                new(ClaimTypes.Name, authResult.Username ?? string.Empty),
                new(ClaimTypes.Role, authResult.RoleID.ToString())

            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
