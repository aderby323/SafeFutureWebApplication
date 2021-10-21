using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SafeFutureWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using SafeFutureWebApplication.Repository;

namespace SafeFutureWebApplication.Controllers
{
    [Authorize("Admin")]
    public class AdminController : Controller
    {
        private TempDB _tempDB;

        public AdminController(TempDB tempDB)
        {
            _tempDB = tempDB;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Reports([FromQuery] string filter, string searchString)
        {
            ViewData["CurrentSearch"] = searchString;
            IEnumerable<Participant> customers = _tempDB.Participants;


            if (!string.IsNullOrEmpty(filter)) { filter.ToLower(); }

            if (!string.IsNullOrEmpty(searchString))
            {
                int household = -1;
                try 
                {
                    household = int.Parse(searchString);
                    customers = customers.Where(x => x.HouseholdSize == household);
                    return View(customers.ToList());

                }
                catch(Exception){ }
                customers = customers.Where(x => x.FirstName.Contains(searchString) || x.ZipCode.Contains(searchString));
            } // end if

            return View(customers.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _tempDB.Users.Add(user);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Edit(User user)
        {
            return View();
        }
        public IActionResult Delete(User user)
        {
            return View();
        }
    }
}
