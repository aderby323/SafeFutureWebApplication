﻿using SafeFutureWebApplication.Repository;
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
            => context.User.ToList();

        public bool CreateUser(User user)
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
        public bool DeleteUser(User user)
        {
            user.Salt = authService.GetSalt();
            user.Password = authService.HashPassword(user.Password, user.Salt);

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
        public User GetUser(string username)
        {
            return context.User.FirstOrDefault(x => x.Username == username);
        }
        public bool EditUser(User user)
        {
            user.Salt = authService.GetSalt();
            user.Password = authService.HashPassword(user.Password, user.Salt);

            try
            {
                context.Update(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
