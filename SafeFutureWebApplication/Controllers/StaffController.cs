using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeFutureWebApplication.Models;
using SafeFutureWebApplication.Services.Interfaces;

namespace SafeFutureWebApplication.Controllers
{
    [Authorize("Staff")]
    public class StaffController : Controller
    {
        private readonly IStaffService StaffService;

        public StaffController(IStaffService StaffService)
        {
            this.StaffService = StaffService;
        }

        public IActionResult Index()
        {
            IEnumerable<Recipient> recipients = StaffService.GetRecipients();
            return View(recipients);
        }

        [HttpGet]
        public IActionResult AddRecipient()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRecipient(Recipient Recipient)
        {
            if (!ModelState.IsValid)
            {
                return View("AddRecipient", Recipient);
            }

            bool result = StaffService.AddRecipient(Recipient);
            if (!result) { return BadRequest(); }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Search([FromQuery] string filter, string searchString)
        {
            ViewData["CurrentSearch"] = searchString;
            IEnumerable<Recipient> recipients = StaffService.GetRecipients();


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
                catch (Exception)
                {

                }
                recipients = recipients.Where(x => x.FirstName.Contains(searchString) || x.LastName.Contains(searchString) || x.ZipCode.Contains(searchString));
            }

            return View(recipients.ToList());
        }

        [HttpGet]
        public IActionResult AddAttendance(Guid recipientId)
        {
            Recipient recipient = StaffService.GetRecipient(recipientId);

            if (recipient is null) { return BadRequest(); }

            ViewBag.Recipient = recipient;
            return View();
        }

        [HttpPost]
        public IActionResult AddAttendance(Attendance attendance)
        {
            attendance.AttendanceId = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return View("AddOrder", attendance);
            }

            bool result = StaffService.AddAttendance(attendance, User.Identity.Name);
            if (!result) { return BadRequest(); }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ViewAttendances(Guid recipientId)
        {
            Recipient recipient = StaffService.GetRecipient(recipientId);

            if (recipient is null) { return BadRequest(); }

            ViewBag.Recipient = recipient;
            IEnumerable<Attendance> orders = StaffService.ViewAttendances(recipientId);

            return View(orders);
        }
    }
}
