using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SafeFutureWebApplication.Repository.Models
{
    public partial class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
    }
}
