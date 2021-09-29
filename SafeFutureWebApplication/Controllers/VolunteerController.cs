using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafeFutureWebApplication.Models;


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
        public IActionResult Register(Volunteer _volunteer)
        {

            return View();
        }
    }
}
