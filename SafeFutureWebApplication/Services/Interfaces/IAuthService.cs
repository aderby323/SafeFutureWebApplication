using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafeFutureWebApplication.Models;
using SafeFutureWebApplication.Models.ViewModels;

namespace SafeFutureWebApplication.Services.Interfaces
{
    public interface IAuthService
    {
        string HashPassword(string password);
        User ValidateLogin(LoginViewModel login);
    }
}
