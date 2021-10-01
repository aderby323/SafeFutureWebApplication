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
        public List<Participant> Participants;
        public List<Customer> Customers;
        public List<User> users = new List<User>();
        public TempDB()
        {
            Users = new List<User>();
            Participants = new List<Participant>();
            Customers = new List<Customer>();

            // WILL ASK CHRIS IF HE WANTS ADMIN TO HAVE ACCESS TO STUFF FUNCTIONALITY
            Users.Add(new User()
            {
                Username = "admin",
                Password = "admin",
                Role = "Admin",
                Roles = { "Admin" }
            });

            Users.Add(new User()
            {
                Username = "staff",
                Password = "staff",
                Role = "Staff",
                Roles = { "Staff" }
            });

            // WILL DELETE BEFORE DELIVERY
            Users.Add(new User()
            {
                Username = "dev",
                Password = "dev",
                Role = "Admin",
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
        public void AddUser(User user)
        {
            users.Add(user);
        }

        
    }
}
