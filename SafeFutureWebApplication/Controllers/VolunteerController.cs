using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SafeFutureWebApplication.Models.ViewModels;

namespace SafeFutureWebApplication.Controllers
{
    public class VolunteerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(ParicipantFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Form not fully filled out.");
            }

            return View();
        }
    }
}
