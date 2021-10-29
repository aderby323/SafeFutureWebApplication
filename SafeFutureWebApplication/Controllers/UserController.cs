using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafeFutureWebApplication.Models;
using SafeFutureWebApplication.Repository;
using Microsoft.AspNetCore.Authorization;


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
            IEnumerable<User> users = _tempDB.Users;
                return View(users.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid) { return View(); }
            _tempDB.Users.Add(user);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            User user = _tempDB.Users.Where(x => x.Username == id).FirstOrDefault();
            if(user is null) { return NotFound($"User with Username: {id} not found."); }
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (!ModelState.IsValid) { return View(user); }
            User oldUser = _tempDB.Users.Where(x => x.Username == user.Username).FirstOrDefault();
     
            Remove(oldUser.Username);
            Create(user);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Remove(string id)
        {
            User user = _tempDB.Users.Where(x => x.Username == (id)).FirstOrDefault();
            if (user is null)
            {return NotFound($"User with Username: {id} not found.");}

            _tempDB.Users.Remove(user);
            return RedirectToAction("Index");
        }

    }
}
