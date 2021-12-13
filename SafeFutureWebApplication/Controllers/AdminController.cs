using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SafeFutureWebApplication.Services.Interfaces;
using SafeFutureWebApplication.Models;

namespace SafeFutureWebApplication.Controllers
{
    [Authorize("Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;
        private readonly IStaffService staffService;

        public AdminController(IAdminService adminService, IStaffService staffService)
        {
            this.adminService = adminService;
            this.staffService = staffService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Reports([FromQuery] string filter, string searchString)
        {
            ViewData["CurrentSearch"] = searchString;
            IEnumerable<Recipient> recipients = staffService.GetRecipients();


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
            }

            return View(recipients.ToList());
        }

        public IActionResult Manage()
        {
            IEnumerable<User> users = adminService.GetUsers();
            return View(users);
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
                bool result = adminService.CreateUser(user, User.Identity.Name);
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

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData["Error"] = "User id is invalid.";
                return RedirectToAction("Manage");
            }

            User result = adminService.GetUserById(id);

            if (result == null) 
            {
                TempData["Error"] = "User id is invalid.";
                return RedirectToAction("Manage");
            }

            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (!ModelState.IsValid) { return View(user); }

            bool result = adminService.UpdateUser(user);

            if (!result) { return View(); }

            return RedirectToAction("Manage");
        }
     
        [HttpPost]
        public IActionResult Remove(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData["Error"] = "User id is invalid.";
                return RedirectToAction("Manage");
            }

            bool result = adminService.DeleteUser(id);

            if (!result) 
            {
                TempData["Error"] = "Unable to remove user from system.";
                return RedirectToAction("Manage"); 
            }

            return RedirectToAction("Manage");
        }
    }
}
