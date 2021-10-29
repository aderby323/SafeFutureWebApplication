using System.Collections.Generic;
using SafeFutureWebApplication.Models;


namespace SafeFutureWebApplication.Repository
{
    public class TempDB
    {
        public List<User> Users;
        public List<Recipient> Recipients;

        public TempDB()
        {
            Users = new List<User>();
            Recipients = new List<Recipient>();

            Users.Add(new User()
            {
                Username = "admin",
                Password = "admin",
                Role = "Admin"
            });

            Users.Add(new User()
            {
                Username = "staff",
                Password = "staff",
                Role = "Staff"
            });

            // WILL DELETE BEFORE DELIVERY
            Users.Add(new User()
            {
                Username = "dev",
                Password = "dev",
                Role = "Dev"
            });

            Recipients.Add(new Recipient()
            {
                FirstName = "Bob",
                LastName = "Johns",
                ZipCode = "32256",
                HouseholdSize = 4,
                Email = "bob.johns@outlook.com",
                ProductsDistributed = { "Diapers", "School Supplies" }
            });

            Recipients.Add(new Recipient()
            {
                FirstName = "Nicole",
                LastName = "Washington",
                ZipCode = "32259",
                HouseholdSize = 6,
                Email = "n.washington@outlook.com",
                ProductsDistributed = { "Food", "Diapers" }
            });

            Recipients.Add(new Recipient()
            {
                FirstName = "Jim",
                LastName = "James",
                ZipCode = "32250",
                HouseholdSize = 2,
                Email = "j.james@gmail.com",
                ProductsDistributed = { "Soap", "Diapers" }
            });

        }

    }
}
