using System;
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
    [Authorize("Staff")]
    public class StaffController : Controller
    {

        private  TempDB _tempDB;

        public StaffController( TempDB tempDB)
        {
            _tempDB = tempDB;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search([FromQuery] string filter, string searchString)
        {
            ViewData["CurrentSearch"] = searchString;
            IEnumerable<Customer> customers = _tempDB.Customers;


            if (!string.IsNullOrEmpty(filter)) { filter.ToLower(); }

            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(x => x.Name.Contains(searchString) || x.Zipcode.Contains(searchString) || x.HouseholdSize.Contains(searchString));
            } // end if

            return View(customers.ToList());
        }

    }
}
