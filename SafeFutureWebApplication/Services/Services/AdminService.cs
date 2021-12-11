using SafeFutureWebApplication.Repository;
using SafeFutureWebApplication.Repository.Models;
using SafeFutureWebApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SafeFutureWebApplication.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext context;
        private readonly IAuthService authService;

        public AdminService(AppDbContext context, IAuthService authService)
        {
            this.context = context;
            this.authService = authService;
        }

        public IEnumerable<User> GetUsers()
            => context.Users.ToList();

        public bool CreateUser(User user, string requester)
        {
            user.Salt = authService.GetSalt();
            user.Password = authService.HashPassword(user.Password, user.Salt);
            
            try
            {
                context.Add(user);
                context.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        bool IAdminService.UpdateUser(User user, string requester)
        {
            throw new NotImplementedException();
        }

        bool IAdminService.DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
