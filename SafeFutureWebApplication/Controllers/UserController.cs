using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafeFutureWebApplication.Models;
using SafeFutureWebApplication.Repository;


namespace SafeFutureWebApplication.Controllers
{
    public class UserController : Controller
    {
        private TempDB _tempDB;
        public UserController(TempDB tempDB)
        {
            _tempDB = tempDB;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            _tempDB.AddUser(user);
            
            return RedirectToAction("Index");
        }
        public IActionResult Edit(User user)
        {
            return View();
        }
        public IActionResult Delete(User user)
        {
            return View();
        }
    }
}
