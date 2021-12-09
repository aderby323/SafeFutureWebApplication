using System.Collections.Generic;
using SafeFutureWebApplication.Models;
using System;
using System.Linq;

namespace SafeFutureWebApplication.Repository
{
    public class TempDB
    {
        private const string SYSTEM = "System";
        public List<User> Users;
        public List<Recipient> Recipients;
        public List<Attendance> Attendances;

        public TempDB()
        {
            Users = new List<User>();
            Recipients = new List<Recipient>();
            Attendances = new List<Attendance>();

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
                RecipientId = Guid.NewGuid(),
                FirstName = "Bob",
                LastName = "Johns",
                ZipCode = "32256",
                HouseholdSize = 4,
                Email = "bob.johns@outlook.com",
                ProductsDistributed = { "Diapers", "School Supplies" }
            });

            Recipients.Add(new Recipient()
            {
                RecipientId = Guid.NewGuid(),
                FirstName = "Nicole",
                LastName = "Washington",
                ZipCode = "32259",
                HouseholdSize = 6,
                Email = "n.washington@outlook.com",
                ProductsDistributed = { "Food", "Diapers" }
            });

            Recipients.Add(new Recipient()
            {
                RecipientId = Guid.NewGuid(),
                FirstName = "Jim",
                LastName = "James",
                ZipCode = "32250",
                HouseholdSize = 2,
                Email = "j.james@gmail.com",
                ProductsDistributed = { "Soap", "Diapers" }
            });

            Attendances.Add(new Attendance()
            {
                AttendanceId = Guid.NewGuid(),
                RecipientId = Recipients.FirstOrDefault(x => x.FirstName == "Jim").RecipientId,
                RecievedItems = true,
                CreatedOn = DateTime.UtcNow.AddDays(-13),
                CreatedBy = SYSTEM
            });

            Attendances.Add(new Attendance()
            {
                AttendanceId = Guid.NewGuid(),
                RecipientId = Recipients.FirstOrDefault(x => x.FirstName == "Jim").RecipientId,
                RecievedItems = true,
                CreatedOn = DateTime.UtcNow.AddDays(-3),
                CreatedBy = SYSTEM
            });

            Attendances.Add(new Attendance()
            {
                AttendanceId = Guid.NewGuid(),
                RecipientId = Recipients.FirstOrDefault(x => x.FirstName == "Nicole").RecipientId,
                RecievedItems = true,
                CreatedOn = DateTime.UtcNow.AddDays(-71),
                CreatedBy = SYSTEM
            });

            Attendances.Add(new Attendance()
            {
                AttendanceId = Guid.NewGuid(),
                RecipientId = Recipients.FirstOrDefault(x => x.FirstName == "Bob").RecipientId,
                RecievedItems = true,
                CreatedOn = DateTime.UtcNow.AddDays(-22),
                CreatedBy = SYSTEM
            });
        }
        public void AddUser(User user)
        {
            Users.Add(user);
        }
        public void RemoveUser(User user)
        {
            Users.Remove(user);
        }
        public void AddUser(User user)
        {
            Users.Add(user);
        }
        public void RemoveUser(User user)
        {
            Users.Remove(user);
        }
    }
}
