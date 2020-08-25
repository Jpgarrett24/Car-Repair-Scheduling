using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CarRepairScheduling.Models;

namespace CarRepairScheduling.Controllers
{
    public class DashboardController : Controller
    {
        private MyContext _context;
        public DashboardController(MyContext context)
        {
            _context = context;
        }
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            Wrapper Wrapper = new Wrapper();
            User ActiveUser = _context.Users.Include(u => u.Cars).ThenInclude(c => c.Services).FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("CurrentUser"));
            if (ActiveUser == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            Wrapper.User = ActiveUser;
            if (ActiveUser.Email.Contains("lubee.com"))
            {
                return View("AdminDashboard", Wrapper);
            }
            else
            {
                return View("Dashboard", Wrapper);
            }
        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "LoginReg");
        }

        [HttpPost("car/create")]
        public IActionResult AddCar(Wrapper Form)
        {
            User ActiveUser = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("CurrentUser"));
            if (ActiveUser == null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                Form.Car.UserId = ActiveUser.UserId;
                _context.Cars.Add(Form.Car);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            else
            {
                return Dashboard();
            }
        }
    }
}