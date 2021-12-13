using SafeFutureWebApplication.Models;
using System;
using System.Collections.Generic;

namespace SafeFutureWebApplication.Services.Interfaces
{
    public interface IStaffService
    {
        IEnumerable<Recipient> GetRecipients();
        IEnumerable<Recipient> GetRecipientsBySearchTerm(string search);
        bool AddRecipient(Recipient Recipient, string requester);
        Recipient GetRecipient(Guid RecipientId);
        bool AddAttendance(Attendance attendance, string requester);
        IEnumerable<Attendance> ViewAttendances(Guid recipientId);
        bool RecipientExists(Guid recipientId);
    }
}
