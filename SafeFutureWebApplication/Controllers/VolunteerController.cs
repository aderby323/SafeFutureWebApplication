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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Volunteer _volunteer)
        {

            return View();
        }
        public IActionResult Edit(Volunteer _volunteer)
        {
            return View();
        }
        public IActionResult Delete(Volunteer _volunteer)
        {
            return View();
        }
    }
}
