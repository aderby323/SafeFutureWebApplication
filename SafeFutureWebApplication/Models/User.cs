using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SafeFutureWebApplication.Models
{
    public class User
    {
        [Required(ErrorMessage = "Please enter a username")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Please enter a password")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Please select a role")]
        public string Role { get; set; }

    }
}
