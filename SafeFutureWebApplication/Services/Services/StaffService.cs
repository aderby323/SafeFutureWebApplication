using SafeFutureWebApplication.Repository;
using SafeFutureWebApplication.Repository.Models;
using SafeFutureWebApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SafeFutureWebApplication.Services
{
    public class StaffService : IStaffService
    {
        private readonly AppDbContext context;

        public StaffService(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Recipient> GetRecipients() => context.Recipients.ToList();

        public IEnumerable<Recipient> GetRecipientsBySearchTerm(string search)
        {
            IQueryable<Recipient> recipients = context.Recipients;

            if (search.IsNullOrWhitespace())
            { 
                return Enumerable.Empty<Recipient>();
            }

            int household;
            if(int.TryParse(search, out household))
            {
                return recipients.Where(x => x.HouseholdSize == household).ToList();
            }

            return recipients.Where(x => x.FirstName == search|| x.LastName == search || x.ZipCode == search).ToList();
        }

        public IEnumerable<Attendance> ViewAttendances(Guid recipientId) => context.Attendances
            .Where(x => x.RecipientId == recipientId)
            .OrderByDescending(x => x.EventDate)
            .ToList();

        public Recipient GetRecipient(Guid recipientId) => context.Recipients.FirstOrDefault(x => x.RecipientId == recipientId);

        public bool AddRecipient(Recipient recipient, string requester)
        {
            recipient.SetModified(requester);

            try
            {
                context.Recipients.Add(recipient);
                context.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool AddAttendance(Attendance attendance, string requester)
        {
            Recipient recipient = context.Recipients.FirstOrDefault(x => x.RecipientId == attendance.RecipientId);
            if (recipient is null) { return false; }

            attendance.EventDate = DateTime.UtcNow;
            attendance.ItemsDistributed = attendance.ItemsDistributed ?? "N/A";
            attendance.ItemsDistributed.Trim();

            attendance.SetModified(requester);

            try
            {
                context.Attendances.Add(attendance);
                context.SaveChanges();
                return true;
            }
            catch(Exception) 
            {
                return false; 
            }
        }

        public bool RecipientExists(Guid recipientId) => context.Recipients.Any(x => x.RecipientId == recipientId);
    }
}
