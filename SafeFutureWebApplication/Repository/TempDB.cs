﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafeFutureWebApplication.Models;


namespace SafeFutureWebApplication.Repository
{
    public class TempDB
    {
        public List<User> Users;

        public TempDB()
        {
            Users = new List<User>();

            Users.Add(new User()
            {
                Username = "admin",
                Password = "admin",
                Role = "admin"
            });

            Users.Add(new User()
            {
                Username = "staff",
                Password = "staff",
                Role = "staff"
            });
        }

    }
}