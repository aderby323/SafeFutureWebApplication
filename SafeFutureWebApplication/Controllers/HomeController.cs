using Microsoft.AspNetCore.Mvc;
using SafeFutureWebApplication.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SafeFutureWebApplication.Models.ViewModels;
using SafeFutureWebApplication.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System;
using SafeFutureWebApplication.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SafeFutureWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthService _authService;
        private readonly AppDbContext context;

        public HomeController(IAuthService authService, AppDbContext context)
        {
            _authService = authService;
            this.context = context;
        }

        [Authorize(Roles = "Staff, Admin, Dev")]
        public async Task<IActionResult> Index()
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == User.Identity.Name);
            if (user is null) { return BadRequest("User is not logged in or not authenticated"); }
            return user.Role switch
            {
                Role.Admin => RedirectToAction("Index", "Admin"),
                Role.Staff => RedirectToAction("Index", "Staff", 1),
                _ => throw new NotImplementedException(),
            };
        }

        [HttpGet]
        public IActionResult Login() => View();
        
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            ViewData["ErrorMessage"] = null;
            User user = _authService.ValidateLogin2(login);
            if (user is null)
            {
                ViewData["ErrorMessage"] = "Username or password is incorrect.";
                return View();
            }

            ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            HttpContext.Session.SetString("SessionKey", login.Username);

            if (user.Role == Role.Admin)
            {
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index", "Staff");
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    }
}
