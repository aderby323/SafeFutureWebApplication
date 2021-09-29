using SafeFutureWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeFutureWebApplication.Services.Interfaces
{
    public interface IVolunteerService
    {
        bool AddParticipant(Participant participant);
        Participant GetParticipant(Guid participantId);
        Participant SearchParticipants(string searchString);
    }
}
