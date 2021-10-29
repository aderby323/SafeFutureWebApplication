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
        public IActionResult Manage()
        {
            IEnumerable<User> users = _tempDB.Users;
            return View(users.ToList());
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
<<<<<<< Updated upstream
  
        public IActionResult Remove(User user)
        {
            //User user = _tempDB.Users.Where(x => @x.Username == username).FirstOrDefault();

          
            //_tempDB.Users.Remove(user);
            _tempDB.RemoveUser(user);
            return RedirectToAction("Index");


=======
        [HttpPost]
        public IActionResult Remove(string id)
        {
            User user = _tempDB.Users.Where(x => x.Username == (id)).FirstOrDefault();
            if (user is null)
            { return NotFound($"User with Username: {id} not found."); }

            _tempDB.Users.Remove(user);
            return RedirectToAction("Index");
>>>>>>> Stashed changes
        }
    }
}
