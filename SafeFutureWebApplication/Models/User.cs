using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SafeFutureWebApplication.Models
{
    public class User
    {
        public User()
        {
            Roles = new List<string>();
        }

        public string Username { get; set; }
        [Required(ErrorMessage ="Please enter a password")]
        public string Password { get; set; }
        public string Role { get; set; }
        public List<string> Roles { get; set; }

    }
}
