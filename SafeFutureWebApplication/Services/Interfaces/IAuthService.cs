using SafeFutureWebApplication.Models.ViewModels;
using SafeFutureWebApplication.Repository.Models;

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
