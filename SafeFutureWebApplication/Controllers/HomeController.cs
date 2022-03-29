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
            if (!login.RecoveryUsername.IsNullOrWhitespace())
            {
                return RedirectToAction("Recovery", "Home", new { username = login.RecoveryUsername });
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
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Recovery(string username)
        {
            if (username.IsNullOrWhitespace())
            {
                ViewData["ErrorMessage"] = "Invalid or malformed username given";
                return Error(ViewData["ErrorMessage"] as string);
            }

            User user = await _authService.GetUser(username);
            if (user is null)
            {
                ViewData["ErrorMessage"] = "Invalid or malformed username given";
                return Error(ViewData["ErrorMessage"] as string);
            }

            
            var viewModel = new PasswordRecoveryViewModel() { Username = username, Question1 = user.Question.Value };
            HttpContext.Session.SetString("User", user.Username);
            HttpContext.Session.SetString("Question1", user.Question.Value);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Recovery(PasswordRecoveryViewModel response)
        {
            var username = HttpContext.Session.GetString("User");
            var question1 = HttpContext.Session.GetString("Question1");

            if (username is null || question1 is null) { return RedirectToAction("Index"); }

            if (response.Question1Response.IsNullOrWhitespace())
            {
                ViewData["ErrorMessage"] = "No response given";
                return View(new PasswordRecoveryViewModel()
                {
                    Username = username,
                    Question1 = question1
                });
            }

            User user = await _authService.GetUser(response.Username);
            if (user is null)
            {
                ViewData["ErrorMessage"] = "Invalid or malformed username given";
                return View(new PasswordRecoveryViewModel()
                {
                    Username = username,
                    Question1 = question1
                });
            }

            bool result = await _authService.ValidatePasswordRecovery(user, response.Question1Response);
            if (!result)
            {
                ViewData["ErrorMessage"] = "Invalid answer(s) provided";
                return View(new PasswordRecoveryViewModel()
                {
                    Username = username,
                    Question1 = question1
                });
            }

            ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            HttpContext.Session.SetString("SessionKey", user.Username);
            HttpContext.Session.Remove("User");
            HttpContext.Session.Remove("Question1");

            return View("NewPassword", new LoginViewModel() { Username = user.Username });
        }

        [HttpPost]
        [Authorize(Roles = "Staff, Admin, Dev")]
        public async Task<IActionResult> NewPassword(LoginViewModel newLogin)
        {
            if (newLogin.Password.IsNullOrWhitespace()) { return BadRequest("No password given"); }
            if (!newLogin.Password.Equals(newLogin.ConfirmPassword)) 
            {
                ViewData["ErrorMessage"] = "Password did not match confirmation";
                return View(new LoginViewModel() { Username = User.Identity.Name }); 
            }

            bool result = await _authService.UpdateUserCredentials(User.Identity.Name, newLogin.Password);
            if (!result)
            {
                ViewData["ErrorMessage"] = "Unable to set new password";
                return View(new LoginViewModel() { Username = User.Identity.Name });
            }

            return RedirectToAction("Index");
        }

        [Route("/error")]
        public IActionResult Error(string error) => Problem(error);
    }
}
