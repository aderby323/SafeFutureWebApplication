using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeFutureWebApplication.Repository.Models;
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

        public IActionResult Index([FromQuery] string searchString)
        {
            IEnumerable<Recipient> recipients = !searchString.IsNullOrWhitespace() ? StaffService.GetRecipientsBySearchTerm(searchString) :StaffService.GetRecipients();
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

            bool result = StaffService.AddRecipient(Recipient, User.Identity.Name);
            if (!result) { return BadRequest(); }

            return RedirectToAction("Index");
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
