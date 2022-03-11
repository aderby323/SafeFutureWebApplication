using System;
using System.ComponentModel.DataAnnotations;

namespace SafeFutureWebApplication.Models
{
    public class User
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(int.MaxValue ,MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
        public string Password { get; set; }
        public string Salt { get; set; }
        public Role Role { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }

        #region Navigation
        public virtual Question Question { get; set; }
        #endregion
    }
}
