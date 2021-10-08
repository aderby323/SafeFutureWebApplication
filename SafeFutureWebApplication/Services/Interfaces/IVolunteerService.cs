using SafeFutureWebApplication.Models;
using System;
using System.Collections.Generic;

namespace SafeFutureWebApplication.Services.Interfaces
{
    public interface IVolunteerService
    {
        IEnumerable<Customer> GetCustomers();
        bool AddCustomer(Customer participant);
        Customer GetCustomer(Guid participantId);
        IEnumerable<Customer> SearchCustomers(string searchString);
    }
}
