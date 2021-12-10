using SafeFutureWebApplication.Repository.Models;
using System;
using System.Collections.Generic;

namespace SafeFutureWebApplication.Services.Interfaces
{
    public interface IAdminService
    {
        /// <summary>Returns all users in the database</summary>
        IEnumerable<User> GetUsers();

        /// <summary>Adds a new user to the database</summary>
        bool CreateUser(User user, string requester);

        /// <summary>Updates an existing user to the database</summary>
        bool UpdateUser(User user, string requester);

        /// <summary>Deletes a user from the database</summary>
        bool DeleteUser(Guid id); 
    }
}
