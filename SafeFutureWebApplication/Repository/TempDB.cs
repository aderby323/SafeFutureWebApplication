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
        public List<User> users = new List<User>();

        public TempDB()
        {
            Users = new List<User>();
            Participants = new List<Participant>();
           
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
        }
        public void AddUser(User user)
        {
            users.Add(user);
        }
    }
}
