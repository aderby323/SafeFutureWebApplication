namespace SafeFutureWebApplication.Models.ViewModels
{
    public class PasswordRecoveryViewModel
    {
        public string Username { get; set; }

        public User User { get; set; }

        public PasswordRecoveryViewModel(){ }

        public PasswordRecoveryViewModel(string username, User user)
        {
            User = user;
            Username = username;
        }
    }
}
