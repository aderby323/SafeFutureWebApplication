using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using SafeFutureWebApplication.Models.ViewModels;
using SafeFutureWebApplication.Repository;
using SafeFutureWebApplication.Models;
using SafeFutureWebApplication.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SafeFutureWebApplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext context;

        public AuthService(AppDbContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public Task<User> GetUser(string username) => context.Users
            .Include(x => x.Question)
            .FirstOrDefaultAsync(u => u.Username == username);

        public async Task<bool> UpdateUserCredentials(string username, string password)
        {
            if ( username.IsNullOrWhitespace() || password.IsNullOrWhitespace()) { return default; }

            User user = context.Users.FirstOrDefault(x => x.Username == username);
            if (user == null) { return default; }

            user.Password = Hash(password, user.Salt);

            try
            {
                context.Users.Update(user);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public string Hash(string input)
        {
            byte[] saltBytes = Convert.FromBase64String(GetSalt());
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                input, saltBytes, KeyDerivationPrf.HMACSHA256,
                100000, 256 / 8));
        }

        /// <inheritdoc/>
        public string Hash(string input, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                input, saltBytes, KeyDerivationPrf.HMACSHA256,
                100000, 256 / 8));
        }

        /// <inheritdoc/>
        public string GetSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        /// <inheritdoc/>
        public User ValidateLogin(LoginViewModel login)
        {
            if (login is null || string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password)) { return default; }

            User user = context.Users.FirstOrDefault(x => x.Username == login.Username);
            if (user == null) { return default; }

            string hash = Hash(login.Password, user.Salt);

            return user.Password.Equals(hash) ? user : null;
        }

        public async Task<bool> ValidatePasswordRecovery(User user, string question1Answer)
        {
            if (question1Answer.IsNullOrWhitespace()) { return false; }

            question1Answer = question1Answer.Trim();
            Question question = await context.Questions.FirstOrDefaultAsync(x => x.QuestionId == user.QuestionId);

            if (question == null) { return false; }

            if (!user.Answer.Equals(question1Answer, StringComparison.InvariantCultureIgnoreCase)) { return false; }

            return true;
        }
    }
}
