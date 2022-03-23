using System.ComponentModel.DataAnnotations;

namespace SafeFutureWebApplication.Models.ViewModels
{
    /// <summary>
    /// Viewmodel for password recovery
    /// </summary>
    public class PasswordRecoveryViewModel
    {
        /// <summary>
        /// Username of the user requesting password recovery
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// First security question tied to user
        /// </summary>
        public string Question1 { get; set; }

        /// <summary>
        /// Response given for answer to first security question
        /// </summary>
        [Required]
        public string Question1Response { get; set; }
    }
}
