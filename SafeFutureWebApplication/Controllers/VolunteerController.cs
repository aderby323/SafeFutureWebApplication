using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeFutureWebApplication.Models;
using SafeFutureWebApplication.Services.Interfaces;

namespace SafeFutureWebApplication.Controllers
{
    [Authorize("Volunteer")]
    public class VolunteerController : Controller
    {
        private readonly IVolunteerService volunteerService;

        public VolunteerController(IVolunteerService volunteerService)
        {
            this.volunteerService = volunteerService;
        }

        public IActionResult Index()
        {
            IEnumerable<Participant> customers = volunteerService.GetParticipants();
            return View(customers);
        }

        [HttpGet]
        public IActionResult AddParticipant()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddParticipant(Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return View("AddParticipant", participant);
            }

            bool result = volunteerService.AddParticipant(participant);
            if (!result) { return BadRequest(); }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Search([FromQuery] string filter, string searchString)
        {
            ViewData["CurrentSearch"] = searchString;
            IEnumerable<Participant> customers = volunteerService.GetParticipants();


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
                catch (Exception)
                {

                }
                customers = customers.Where(x => x.FirstName.Contains(searchString) || x.LastName.Contains(searchString) || x.ZipCode.Contains(searchString));
            }

            return View(customers.ToList());
        }
    }
}
