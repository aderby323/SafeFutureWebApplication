using SafeFutureWebApplication.Models;
using SafeFutureWebApplication.Models.ViewModels;

namespace SafeFutureWebApplication.Services.Interfaces
{
    public interface IAuthService
    {
        string HashPassword(string password, string salt);
        string GetSalt();
        User ValidateLogin(LoginViewModel login);
        User ValidateLogin2(LoginViewModel login);

    }
}
