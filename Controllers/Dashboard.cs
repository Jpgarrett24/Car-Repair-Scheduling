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
            User ActiveUser = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("CurrentUser"));
            if (ActiveUser == null)
            {
                return RedirectToAction("Index");
            }
            Wrapper.User = ActiveUser;
            if (ActiveUser.UserId == 1)
            {
                return View("AdminDashboard", Wrapper);
            }
            else
            {
                return View("Dashboard", Wrapper);
            }
        }
    }
}