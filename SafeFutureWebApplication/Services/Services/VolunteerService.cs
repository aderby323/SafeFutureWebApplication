using SafeFutureWebApplication.Models;
using SafeFutureWebApplication.Repository;
using SafeFutureWebApplication.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace SafeFutureWebApplication.Services
{
    public class VolunteerService : IVolunteerService
    {
        private readonly TempDB repo;
        public VolunteerService(TempDB tempDb)
        {
            repo = tempDb;
        }

        public IEnumerable<Participant> GetParticipants()
        {
            return repo.Participants;
        }
            
        public bool AddParticipant(Participant participant)
        {
            participant.ParticipantId = Guid.NewGuid();
            try
            {
                repo.Participants.Add(participant);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        Participant IVolunteerService.GetParticipant(Guid participantId)
        {
            throw new NotImplementedException();
        }

        Participant IVolunteerService.SearchParticipants(string searchString)
        {
            throw new NotImplementedException();
        }
    }
}
