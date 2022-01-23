using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using SafeFutureWebApplication.Models.ViewModels;
using SafeFutureWebApplication.Repository;
using SafeFutureWebApplication.Models;
using SafeFutureWebApplication.Services.Interfaces;

namespace SafeFutureWebApplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext context;

        public AuthService(AppDbContext context)
        {
            this.context = context;
        }

        public string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password, saltBytes, KeyDerivationPrf.HMACSHA256,
                100000, 256 / 8));
        }

        public string GetSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public User ValidateLogin(LoginViewModel login)
        {
            if (login is null || login.Username.IsNullOrWhitespace() || login.Password.IsNullOrWhitespace()) { return default; }

            User user = context.Users.FirstOrDefault(x => x.Username.Equals(login.Username) && x.Password.Equals(login.Password));

            return user ?? null;
        }

        // Password and Salt testing
        public User ValidateLogin2(LoginViewModel login)
        {
            if (login is null || string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password)) { return default; }

            User user = context.Users.FirstOrDefault(x => x.Username == login.Username);
            if (user == null) { return default; }

            string hash = HashPassword(login.Password, user.Salt);

            return user.Password.Equals(hash) ? user : null;
        }
    }
}
