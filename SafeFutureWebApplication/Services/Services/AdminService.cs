using SafeFutureWebApplication.Repository;
using SafeFutureWebApplication.Repository.Models;
using SafeFutureWebApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Ganss.XSS;
using Microsoft.EntityFrameworkCore;

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

        public IEnumerable<User> GetUsers() => context.Users.AsNoTracking().ToList();

        public User GetUserById(Guid id) => context.Users.AsNoTracking().FirstOrDefault(x => x.UserId == id);

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

        public bool UpdateUser(User update)
        {
            User existing = context.Users.FirstOrDefault(x => x.UserId == update.UserId);

            if (existing == null) { return false; }

            existing.Username = SanitizeText(update.Username);
            existing.Role = SanitizeText(update.Role);

            try
            {
                context.Update(existing);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUser(Guid id)
        {
            User user = context.Users.FirstOrDefault(x => x.UserId == id);

            if (user == null) { return false; }

            try
            {
                context.Remove(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string SanitizeText(string value)
        {
            HtmlSanitizer sanitizer = new HtmlSanitizer();
            return sanitizer.Sanitize(value);
        }

}
}
