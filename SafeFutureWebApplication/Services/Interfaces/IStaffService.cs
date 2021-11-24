using SafeFutureWebApplication.Models;
using System;
using System.Collections.Generic;

namespace SafeFutureWebApplication.Services.Interfaces
{
    public interface IStaffService
    {
        IEnumerable<Recipient> GetRecipients();
        bool AddRecipient(Recipient Recipient);
        Recipient GetRecipient(Guid RecipientId);
        IEnumerable<Recipient> SearchRecipients(string searchString);
        bool AddAttendance(Attendance attendance, string requester);
        IEnumerable<Attendance> ViewAttendances(Guid recipientId);
        bool RecipientExists(Guid recipientId);
    }
}
