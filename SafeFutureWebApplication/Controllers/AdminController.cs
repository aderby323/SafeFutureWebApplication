using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SafeFutureWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using SafeFutureWebApplication.Repository;
using SafeFutureWebApplication.Services.Interfaces;
using SafeFutureWebApplication.Repository.Models;

namespace SafeFutureWebApplication.Controllers
{
    [Authorize("Admin")]
    public class AdminController : Controller
    {
        private TempDB _tempDB;
        private readonly IAdminService adminService;

        public AdminController(TempDB tempDB, IAdminService adminService)
        {
            _tempDB = tempDB;
            this.adminService = adminService;
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
        public IActionResult Manage()
        {
            IEnumerable<Repository.Models.User> users = adminService.GetUsers();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Repository.Models.User user)
        {
            if (ModelState.IsValid)
            {
                bool result = adminService.CreateUser(user);
                if (!result)
                {
                    return View();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Edit(Models.User user)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Remove(string id)
        {
            Models.User user = _tempDB.Users.Where(x => x.Username == (id)).FirstOrDefault();
            if (user is null)
            { return NotFound($"User with Username: {id} not found."); }

            _tempDB.Users.Remove(user);
            return RedirectToAction("Manage");
        }
    }
}
