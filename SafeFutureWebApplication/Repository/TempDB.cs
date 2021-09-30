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

        public TempDB()
        {
            Users = new List<User>();

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
        }

    }
}
