using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SafeFutureWebApplication.Services.Interfaces;
using SafeFutureWebApplication.Models;
using System.Globalization;
using System.IO;

namespace SafeFutureWebApplication.Controllers
{
    [Authorize("Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;
        private readonly IStaffService staffService;
        private readonly CultureInfo enUs = new CultureInfo("en-US");

        public AdminController(IAdminService adminService, IStaffService staffService)
        {
            this.adminService = adminService;
            this.staffService = staffService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Reports([FromQuery] string searchString)
        {
            ViewData["CurrentSearch"] = searchString;
            if (!string.IsNullOrEmpty(searchString)) { searchString.ToLower(); }
            (IEnumerable<Recipient>, int maxPages) recipients = staffService.GetRecipients(searchString, 0);

            if (!string.IsNullOrEmpty(searchString))
            {
                int household = -1;
                try 
                {
                    household = int.Parse(searchString);
                    recipients.Item1 = recipients.Item1.Where(x => x.HouseholdSize == household);
                    return View(recipients.Item1.ToList());

                }
                catch(Exception){ }
                recipients.Item1 = recipients.Item1.Where(x => x.FirstName.Contains(searchString) || x.ZipCode.Contains(searchString));
            }

            return View(recipients.Item1.ToList());
        }

        [HttpGet]
        public IActionResult Report([FromQuery(Name = "fromDate")] string fromDate, [FromQuery(Name = "toDate")] string toDate)
        {
            if (!DateTime.TryParse(fromDate, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime from))
            {
                from = DateTime.MinValue;
            }
            if (!DateTime.TryParse(toDate, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime to))
            {
                to = DateTime.MinValue;
            }

            if (to <= from)
            {
                return BadRequest("Invalid date range provided");
            }

            byte[] ReportData = adminService.GetReport(from, to);

            Response.StatusCode = 201;
            return File(ReportData, "text/csv");
        }

        // COLT REPORT TESTING
        public IActionResult ColtReportTesting()
        {
            byte[] ReportData = adminService.ColtReportTesting();

            return File(ReportData, "text/csv", "testReport.csv");

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
