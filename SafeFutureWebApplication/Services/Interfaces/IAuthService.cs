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
        /// <returns>User if user exists in the database. Otherwise, <see langword="null"/>.</returns>
        Task<User> GetUser(string username);

        /// <summary>
        /// Updates a user's credentials after password recovery
        /// </summary>
        /// <param name="username">The username of the requested user</param>
        /// <param name="password">New password for the user</param>
        /// <returns><see langword="true"/> if the method successfully updates the user's password. Otherwise, <see langword="false"/></returns>
        Task<bool> UpdateUserCredentials(string username, string password);

        /// <summary>
        /// Hashes an inputed <see cref="string"/>
        /// </summary>
        /// <param name="input">The string to be hashed</param>
        /// <returns>Hashed string</returns>
        string Hash(string input);

        /// <summary>
        /// Hashes an inputed <see cref="string"/> with a provided salt
        /// </summary>
        /// <param name="input">The string to be hashed</param>
        /// <param name="salt">Provided salt to use when hashing</param>
        /// <returns>Hashed string</returns>
        string Hash(string input, string salt);

        /// <summary>
        /// Generates a new Base-64 salt
        /// </summary>
        /// <returns>Generated salt</returns>
        string GetSalt();

        /// <summary>
        /// Validates the provided <see cref="LoginViewModel"/>
        /// </summary>
        /// <param name="login">The login viewmodel with username and password</param>
        /// <returns><see cref="User"/> if validation is successful. Otherwise, <see langword="null"/></returns>
        User ValidateLogin(LoginViewModel login);

        /// <summary>
        /// Validates the provided <see cref="User"/> and a provided answer
        /// </summary>
        /// <param name="user">The user used for validation</param>
        /// <param name="questionAnswer">The answer used to compare against the user's answer</param>
        /// <returns><see langword="true"/> if validation is successful. Otherwise, <see langword="false"/></returns>
        Task<bool> ValidatePasswordRecovery(User user, string questionAnswer);
    }
}
