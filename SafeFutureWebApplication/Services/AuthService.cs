using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafeFutureWebApplication.Models;
using SafeFutureWebApplication.Models.ViewModels;
using SafeFutureWebApplication.Repository;
using SafeFutureWebApplication.Services.Interfaces;

namespace SafeFutureWebApplication.Services
{
    public class AuthService : IAuthService
    {
        private TempDB _tempDB;

        public AuthService(TempDB tempDB)
        {
            _tempDB = tempDB;
        }

        public string HashPassword(string password)
        {
            throw new System.NotImplementedException();
        }

        public User ValidateLogin(LoginViewModel login)
        {
            if (login is null || string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password)) { return default; }

            User user = _tempDB.Users.Find(x => x.Username.Equals(login.Username));
            User user2 = _tempDB.users.Find(x => x.Username.Equals(login.Username));

            if (user is null && user2 is null) { return default; }
            if (!(user is null))
            {
                if (user.Password.Equals(login.Password))
                {
                    return user;
                }
            }
            if (user2.Password.Equals(login.Password))
            {
                return user2;
            }
            else
            {
                return default;
            }
        }

    }
}
