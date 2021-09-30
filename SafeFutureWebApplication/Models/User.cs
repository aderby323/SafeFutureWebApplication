using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeFutureWebApplication.Models
{
    public class User
    {
        public User()
        {
            Roles = new List<string>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }

    }
}
