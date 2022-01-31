using SafeFutureWebApplication.Models;
using System;
using System.Collections.Generic;

namespace SafeFutureWebApplication.Services.Interfaces
{
    public interface IStaffService
    {
        (IEnumerable<Recipient>, int numOfPages) GetRecipients(string search, int page);
        bool AddRecipient(Recipient Recipient, string requester);
        Recipient GetRecipient(Guid RecipientId);
        bool AddAttendance(Attendance attendance, string requester);
        IEnumerable<Attendance> ViewAttendances(Guid recipientId);
    }
}
