using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeFutureWebApplication.Models;
using SafeFutureWebApplication.Services.Interfaces;

namespace SafeFutureWebApplication.Controllers
{
    [Authorize("Admin")]
    public class VolunteerController : Controller
    {
        private readonly IVolunteerService volunteerService;

        public VolunteerController(IVolunteerService volunteerService)
        {
            this.volunteerService = volunteerService;
        }

        public IActionResult Index()
        {
            IEnumerable<Customer> customers = volunteerService.GetCustomers();
            return View(customers);
        }

        [HttpGet]
        public IActionResult AddParticipant()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddParticipant(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View("AddParticipant", customer);
            }

            bool result = volunteerService.AddCustomer(customer);
            if (!result) { return BadRequest(); }

            return RedirectToAction("Index");
        }
    }
}
