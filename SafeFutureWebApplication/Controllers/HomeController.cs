using Microsoft.AspNetCore.Mvc;
using SafeFutureWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SafeFutureWebApplication.Models.ViewModels;
using SafeFutureWebApplication.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Threading.Tasks;

namespace SafeFutureWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthService _authService;

        public HomeController(IAuthService authService)
        {
            _authService = authService;
        }

        [Authorize(Roles = "Staff, Admin, Dev")]
        public async Task<IActionResult> Index()
        {
            var user = await _authService.GetUser(User.Identity.Name);
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
            if (login.ForgotPassword)
            {
                return RedirectToAction("Recovery", login.Username);
            }
            User user = _authService.ValidateLogin(login);
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

        [HttpGet]
        public async Task<IActionResult> RecoveryAsync(string username) 
        {
            if (username.IsNullOrWhitespace())
            {
                ViewData["ErrorMessage"] = "Invalid or malformed username given";
                return View();
            }

            User user = await _authService.GetUser(username);
            if (user is null)
            {
                ViewData["ErrorMessage"] = "Invalid or malformed username given";
                return View();
            }

            if (user.QuestionId == 0)
            {
                ViewData["ErrorMessage"] = "User does not have any security questions";
                return View();
            }

            var viewModel = new PasswordRecoveryViewModel() { Username = username, Question1 = user.Question.Value };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Recovery(PasswordRecoveryViewModel response)
        {
            if (response.Question1Response.IsNullOrWhitespace())
            {
                ViewData["ErrorMessage"] = "No repsonse given";
                return View();
            }

            User user = await _authService.GetUser(response.Username);
            if (user is null)
            {
                ViewData["ErrorMessage"] = "Invalid or malformed username given";
                return View();
            }

            bool result = await _authService.ValidatePasswordRecovery(user, response.Question1Response);
            if (!result)
            {
                ViewData["ErrorMessage"] = "Invalid answer given";
                return View();
            }
            //TODO: Add create new password page
            return View();
        }

        public IActionResult Privacy() => View();

        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
