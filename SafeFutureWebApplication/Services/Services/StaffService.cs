using SafeFutureWebApplication.Repository;
using SafeFutureWebApplication.Models;
using SafeFutureWebApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SafeFutureWebApplication.Services
{
    public class StaffService : IStaffService
    {
        private readonly AppDbContext context;
        private const int DEFAULT_PAGE_SIZE = 5;

        public StaffService(AppDbContext context)
        {
            this.context = context;
        }

        public (IEnumerable<Recipient>, int numOfPages) GetRecipients(string search, int page = 1)
        {
            IQueryable<Recipient> query = context.Recipients.AsQueryable();

            if (page < 1) { page = 1; }
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search) || x.ZipCode.Contains(search));
            }

            IEnumerable<Recipient> recipients = query
                .OrderBy(x => x.LastName)
                .AsNoTracking()
                .ToList();

            decimal pages = (recipients.Count() / DEFAULT_PAGE_SIZE) + 1;
            int maxPages = (int)Math.Ceiling(pages);
            return (recipients.Skip((page - 1) * DEFAULT_PAGE_SIZE).Take(DEFAULT_PAGE_SIZE), maxPages);

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
    }
}
