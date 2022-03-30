using System;
using System.ComponentModel.DataAnnotations;

namespace SafeFutureWebApplication.Models
{
    public class User
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 200 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(int.MaxValue ,MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }
        public string Salt { get; set; }
        public Role Role { get; set; }
        [Required(ErrorMessage = "Security question is required")]
        public int QuestionId { get; set; }
        [Required(ErrorMessage = "Valid answer is required")]
        public string Answer { get; set; }

        #region Navigation
        public virtual Question Question { get; set; }
        #endregion
    }
}
