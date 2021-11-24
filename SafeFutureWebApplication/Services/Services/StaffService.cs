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
            Recipient.RecipientId = Guid.NewGuid();
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
            return repo.Recipients.FirstOrDefault(x => x.RecipientId == recipientId);
        }

        public IEnumerable<Recipient> SearchRecipients(string searchString)
        {
            if (searchString.IsNullOrWhitespace()) { return Enumerable.Empty<Recipient>(); }

            searchString.ToLower();
            return repo.Recipients
                .Where(x => x.FirstName == searchString || x.LastName == searchString)
                .ToList();
        }

        public bool AddAttendance(Attendance attendance, string requester)
        {
            Recipient recipient = repo.Recipients.FirstOrDefault(x => x.RecipientId == attendance.RecipientId);
            if (recipient is null) { return false; }

            attendance.SetModified(requester);

            try
            {
                repo.Attendances.Add(attendance);
            }
            catch(Exception) { return false; }

            return true;
        }

        public IEnumerable<Attendance> ViewAttendances(Guid recipientId)
        {
            return repo.Attendances.Where(x => x.RecipientId == recipientId).ToList();
        }

        public bool RecipientExists(Guid recipientId)
        {
            return repo.Recipients.Any(x => x.RecipientId == recipientId);
        }
    }
}
