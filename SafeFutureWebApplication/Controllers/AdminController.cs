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
            IEnumerable<Recipient> recipients = _tempDB.Recipients;


            if (!string.IsNullOrEmpty(filter)) { filter.ToLower(); }

            if (!string.IsNullOrEmpty(searchString))
            {
                int household = -1;
                try 
                {
                    household = int.Parse(searchString);
                    recipients = recipients.Where(x => x.HouseholdSize == household);
                    return View(recipients.ToList());

                }
                catch(Exception){ }
                recipients = recipients.Where(x => x.FirstName.Contains(searchString) || x.ZipCode.Contains(searchString));
            } // end if

            return View(recipients.ToList());
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
