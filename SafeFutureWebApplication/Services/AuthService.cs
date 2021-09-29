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

        // COLT COME BACK HERE

    }
}
