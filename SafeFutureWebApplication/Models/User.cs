using System.ComponentModel.DataAnnotations;

namespace SafeFutureWebApplication.Models
{
    public class User
    {
        [Required(ErrorMessage ="Please enter a username")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Please enter a password")]
        public string Password { get; set; }

        public string Salt { get; set; }

        public string Role { get; set; }

    }
}
