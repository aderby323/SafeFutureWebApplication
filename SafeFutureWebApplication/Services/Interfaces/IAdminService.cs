using SafeFutureWebApplication.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeFutureWebApplication.Services.Interfaces
{
    public interface IAdminService
    {
        IEnumerable<User> GetUsers();

        bool CreateUser(User user);
        bool DeleteUser(User user);
        bool EditUser(User user);
        User GetUser(string username);
    }
}
