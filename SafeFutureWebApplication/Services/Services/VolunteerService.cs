using SafeFutureWebApplication.Models;
using SafeFutureWebApplication.Repository;
using SafeFutureWebApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
            participant.CustomerId = Guid.NewGuid();
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

        public Participant GetParticipant(Guid customerId)
        {
            return repo.Participants.FirstOrDefault(x => x.CustomerId == customerId);
        }

        public IEnumerable<Participant> SearchParticipants(string searchString)
        {
            if (searchString.IsNullOrWhitespace()) { return Enumerable.Empty<Participant>(); }

            searchString.ToLower();
            return repo.Participants
                .Where(x => x.FirstName == searchString || x.LastName == searchString)
                .ToList();
        }
    }
}
