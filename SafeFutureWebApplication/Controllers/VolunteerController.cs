using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeFutureWebApplication.Models;
using SafeFutureWebApplication.Repository;

namespace SafeFutureWebApplication.Controllers
{
    [Authorize("Admin")]
    public class VolunteerController : Controller
    {
        private readonly TempDB provider;

        public VolunteerController(TempDB provider)
        {
            this.provider = provider;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return View(participant);
            }

            participant.ParticipantId = Guid.NewGuid();
            provider.Participants.Add(participant);

            return View();
        }
    }
}
