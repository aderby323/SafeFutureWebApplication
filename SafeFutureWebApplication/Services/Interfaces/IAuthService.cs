using SafeFutureWebApplication.Models.ViewModels;
using SafeFutureWebApplication.Models;
using System.Threading.Tasks;

namespace SafeFutureWebApplication.Services.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Gets the requested user from the database, if they exist
        /// </summary>
        /// <param name="username"></param>
        /// <returns>User if user exists in the database. Otherwise, null.</returns>
        Task<User> GetUser(string username);
        Task<bool> UpdateUser(string username, LoginViewModel login);
        string HashPassword(string password, string salt);
        string GetSalt();
        User ValidateLogin(LoginViewModel login);
        Task<bool> ValidatePasswordRecovery(User user, string questionAnswer);
    }
}
