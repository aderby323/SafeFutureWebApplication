using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeFutureWebApplication.Models;
using SafeFutureWebApplication.Models.ViewModels;
using SafeFutureWebApplication.Services.Interfaces;

namespace SafeFutureWebApplication.Controllers
{
    [Authorize(Roles = "Staff, Dev, Admin")]
    public class StaffController : Controller
    {
        private readonly IStaffService StaffService;

        public StaffController(IStaffService StaffService)
        {
            this.StaffService = StaffService;
        }

        public IActionResult Index(string searchString, int page = 1)
        {
            ViewData["CurrentSearch"] = searchString;


            (IEnumerable<Recipient>, int) recipients = StaffService.GetRecipients(searchString, page);
            var viewModel = new GetRecipientsViewModel(recipients.Item1, page, recipients.Item2);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddRecipient()
        {
            return PartialView("_AddRecipientPartial");
        }

        [HttpPost]
        public IActionResult AddRecipient(Recipient Recipient)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ValidRecipient = "False";
                return PartialView("_AddRecipientPartial", Recipient);
            }

            bool result = StaffService.AddRecipient(Recipient, User.Identity.Name);
            if (!result) 
            { 
                return PartialView("_AddRecipientPartial", Recipient);
            }

            ViewBag.ValidRecipient = "True";
            return PartialView("_AddRecipientPartial");
        }

        [HttpGet]
        public IActionResult AddAttendance(Guid recipientId)
        {
            Recipient recipient = StaffService.GetRecipient(recipientId);

            if (recipient is null) { return BadRequest(); }

            ViewBag.Recipient = recipient;
            return PartialView("_AddAttendancePartial");
        }

        [HttpPost]
        public IActionResult AddAttendance(Attendance attendance)
        {
            attendance.AttendanceId = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return PartialView("_AddAttendancePartial", attendance);
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
