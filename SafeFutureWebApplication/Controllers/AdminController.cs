﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SafeFutureWebApplication.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SafeFutureWebApplication.Models.ViewModels;
using SafeFutureWebApplication.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using SafeFutureWebApplication.Repository;
using Microsoft.AspNetCore.Authentication;

namespace SafeFutureWebApplication.Controllers
{
    [Authorize("Admin")]
    public class AdminController : Controller
    {
        private TempDB _tempDB;

        public AdminController(TempDB tempDB)
        {
            _tempDB = tempDB;
        }


        public IActionResult Index()
        {
            return View();
        }

        /*

        public IActionResult Reports()
        {
            return View();
        }

        */

        public IActionResult Reports([FromQuery] string filter, string searchString)
        {
            ViewData["CurrentSearch"] = searchString;
            IEnumerable<Customer> customers = _tempDB.Customers;


            if (!string.IsNullOrEmpty(filter)) { filter.ToLower(); }

            if (!string.IsNullOrEmpty(searchString))
            {
                int household = -1;
                try 
                {
                    household = int.Parse(searchString);
                    customers = customers.Where(x => x.HouseholdSize == household);
                    return View(customers.ToList());

                }
                catch(Exception){ }
                customers = customers.Where(x => x.FirstName.Contains(searchString) || x.ZipCode.Contains(searchString));
            } // end if

            return View(customers.ToList());
        }
    }
}
