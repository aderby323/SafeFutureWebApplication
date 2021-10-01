using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafeFutureWebApplication.Models;


namespace SafeFutureWebApplication.Repository
{
    public class TempDB
    {
        public List<User> Users;
        public List<Customer> Customers;

        public TempDB()
        {
            Users = new List<User>();
            Customers = new List<Customer>();

            // WILL ASK CHRIS IF HE WANTS ADMIN TO HAVE ACCESS TO STUFF FUNCTIONALITY
            Users.Add(new User()
            {
                Username = "admin",
                Password = "admin",
                Roles = { "Admin" }
            });

            Users.Add(new User()
            {
                Username = "staff",
                Password = "staff",
                Roles = { "Staff" }
            });

            // WILL DELETE BEFORE DELIVERY
            Users.Add(new User()
            {
                Username = "dev",
                Password = "dev",
                Roles = { "Staff", "Admin" }
            });

            Customers.Add(new Customer()
            {
                Name = "Bob Johns",
                Zipcode = "32256",
                HouseholdSize = "4",
                Email = "bob.johns@outlook.com",
                ProductsDistributed = {"Diapers", "School Supplies"}
            });

            Customers.Add(new Customer()
            {
                Name = "Nicole Washington",
                Zipcode = "32259",
                HouseholdSize = "6",
                Email = "n.washington@outlook.com",
                ProductsDistributed = { "Food", "Diapers" }
            });

            Customers.Add(new Customer()
            {
                Name = "Jim James",
                Zipcode = "32250",
                HouseholdSize = "2",
                Email = "j.james@gmail.com",
                ProductsDistributed = { "Soap", "Diapers" }
            });



        }

    }
}
