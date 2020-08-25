using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CarRepairScheduling.Models;

namespace CarRepairScheduling.Controllers
{
    public class LoginRegController : Controller
    {
        private MyContext _context;
        public LoginRegController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            Wrapper Wrapper = new Wrapper();
            return View("Index", Wrapper);
        }
        [HttpPost("")]
        public IActionResult Register(Wrapper Form)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == Form.User.Email))
                {
                    ModelState.AddModelError("User.Email", "Email already registered");
                    return Index();
                }

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                Form.User.Password = Hasher.HashPassword(Form.User, Form.User.Password);
                _context.Add(Form.User);
                _context.SaveChanges();

                User NewUser = _context.Users.FirstOrDefault(u => u.Email == Form.User.Email);
                int UserId = NewUser.UserId;
                HttpContext.Session.SetInt32("CurrentUser", UserId);
                return RedirectToAction("Dashboard");
            }
            else
            {
                return Index();
            }
        }
        [HttpPost("login")]
        public IActionResult Login(Wrapper Form)
        {
            if (ModelState.IsValid)
            {
                User ReturningUser = _context.Users.FirstOrDefault(u => u.Email == Form.LoginUser.LoginEmail);
                if (ReturningUser == null)
                {
                    ModelState.AddModelError("LoginUser.LoginEmail", "Invalid Email Address/Password");
                    return Index();
                }

                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(Form.LoginUser, ReturningUser.Password, Form.LoginUser.LoginPassword);
                if (result == 0)
                {
                    ModelState.AddModelError("LoginUser.LoginEmail", "Invalid Email Address/Password");
                    return Index();
                }

                HttpContext.Session.SetInt32("CurrentUser", ReturningUser.UserId);
                return RedirectToAction("Dashboard");
            }
            else
            {
                return Index();
            }
        }
    }
}