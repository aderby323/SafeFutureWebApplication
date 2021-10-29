using SafeFutureWebApplication.Models;
using SafeFutureWebApplication.Repository;
using SafeFutureWebApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SafeFutureWebApplication.Services
{
    public class StaffService : IStaffService
    {
        private readonly TempDB repo;
        public StaffService(TempDB tempDb)
        {
            repo = tempDb;
        }

        public IEnumerable<Recipient> GetRecipients()
        {
            return repo.Recipients;
        }
            
        public bool AddRecipient(Recipient Recipient)
        {
            Recipient.recipientId = Guid.NewGuid();
            Recipient.SetModified(Recipient.recipientId);
            try
            {
                repo.Recipients.Add(Recipient);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public Recipient GetRecipient(Guid recipientId)
        {
            return repo.Recipients.FirstOrDefault(x => x.recipientId == recipientId);
        }

        public IEnumerable<Recipient> SearchRecipients(string searchString)
        {
            if (searchString.IsNullOrWhitespace()) { return Enumerable.Empty<Recipient>(); }

            searchString.ToLower();
            return repo.Recipients
                .Where(x => x.FirstName == searchString || x.LastName == searchString)
                .ToList();
        }
    }
}
