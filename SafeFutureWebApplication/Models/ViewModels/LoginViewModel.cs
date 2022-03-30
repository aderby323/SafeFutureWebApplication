
namespace SafeFutureWebApplication.Models.ViewModels
{
    public class LoginViewModel
    {
        public string Username { get; set; }

        //TODO: Enforce minimum password length
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string RecoveryUsername { get; set; }
    }
}
