using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafeFutureWebApplication.Models;


namespace SafeFutureWebApplication.Controllers
{
    public class UserController : Controller
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
        public IActionResult Create(User _volunteer)
        {

            return View();
        }
        public IActionResult Edit(User _volunteer)
        {
            return View();
        }
        public IActionResult Delete(User _volunteer)
        {
            return View();
        }
    }
}
