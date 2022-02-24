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
        public Task<User> GetUser(string username) => context.Users.FirstOrDefaultAsync(u => u.Username == username);

        /// <inheritdoc/>
        public string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password, saltBytes, KeyDerivationPrf.HMACSHA256,
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

            string hash = HashPassword(login.Password, user.Salt);

            return user.Password.Equals(hash) ? user : null;
        }
    }
}
