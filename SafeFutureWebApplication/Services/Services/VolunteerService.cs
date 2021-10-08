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

        public IEnumerable<Customer> GetCustomers()
        {
            return repo.Customers;
        }
            
        public bool AddCustomer(Customer customer)
        {
            customer.CustomerId = Guid.NewGuid();
            try
            {
                repo.Customers.Add(customer);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public Customer GetCustomer(Guid customerId)
        {
            return repo.Customers.FirstOrDefault(x => x.CustomerId == customerId);
        }

        public IEnumerable<Customer> SearchCustomers(string searchString)
        {
            throw new NotImplementedException();
        }
    }
}
