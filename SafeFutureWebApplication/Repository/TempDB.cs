using System.Collections.Generic;
using SafeFutureWebApplication.Models;


namespace SafeFutureWebApplication.Repository
{
    public class TempDB
    {
        public List<User> Users;
        public List<Participant> Participants;

        public TempDB()
        {
            Users = new List<User>();
            Participants = new List<Participant>();

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
                Roles = { "Volunteer", "Admin" }
            });

            Participants.Add(new Participant()
            {
                FirstName = "Bob",
                LastName = "Johns",
                ZipCode = "32256",
                HouseholdSize = 4,
                Email = "bob.johns@outlook.com",
                ProductsDistributed = { "Diapers", "School Supplies" }
            });

            Participants.Add(new Participant()
            {
                FirstName = "Nicole",
                LastName = "Washington",
                ZipCode = "32259",
                HouseholdSize = 6,
                Email = "n.washington@outlook.com",
                ProductsDistributed = { "Food", "Diapers" }
            });

            Participants.Add(new Participant()
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
