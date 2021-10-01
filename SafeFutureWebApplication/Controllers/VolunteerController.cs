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
            IEnumerable<Participant> participants = volunteerService.GetParticipants();
            return View(participants);
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
    }
}
