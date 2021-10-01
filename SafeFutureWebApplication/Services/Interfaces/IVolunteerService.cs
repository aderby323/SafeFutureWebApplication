using SafeFutureWebApplication.Models;
using System;
using System.Collections.Generic;

namespace SafeFutureWebApplication.Services.Interfaces
{
    public interface IVolunteerService
    {
        IEnumerable<Participant> GetParticipants();
        bool AddParticipant(Participant participant);
        Participant GetParticipant(Guid participantId);
        Participant SearchParticipants(string searchString);
    }
}
