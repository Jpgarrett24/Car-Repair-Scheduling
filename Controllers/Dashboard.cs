using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
        [HttpGet("service")]
        public IActionResult Services()
        {
            Wrapper Wrapper = new Wrapper();
            User ActiveUser = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("CurrentUser"));
            if (ActiveUser == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            Wrapper.User = ActiveUser;
            Wrapper.AllServiceTypes = _context.ServiceTypes.Include(s => s.Services).OrderBy(s => s.Name).ToList();
            return View("Services", Wrapper);
        }
        [HttpPost("service/delete/{id}")]
        public IActionResult DeleteService(int id)
        {
            ServiceType ToDelete = _context.ServiceTypes.FirstOrDefault(s => s.ServiceTypeId == id);
            _context.Remove(ToDelete);
            _context.SaveChanges();
            return RedirectToAction("Services");
        }

        [HttpGet("service/new")]
        public IActionResult NewService()
        {
            User ActiveUser = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("CurrentUser"));
            if (ActiveUser == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            return View("ServiceTypesForm");
        }
        [HttpPost("service/new")]
        public IActionResult CreateServiceType(Wrapper form)
        {
            if (ModelState.IsValid)
            {
                ServiceType newServiceType = form.ServiceType;
                _context.ServiceTypes.Add(newServiceType);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            else
            {
                return Services();
            }
        }

        [HttpGet("car/{id}")]
        public IActionResult CarDetails(int id)
        {
            Wrapper Wrapper = new Wrapper();
            Wrapper.Car = _context.Cars.Include(c => c.Services).ThenInclude(s => s.ServiceType).FirstOrDefault(c => c.CarId == id);
            User ActiveUser = _context.Users.Include(u => u.Cars).ThenInclude(c => c.Services).FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("CurrentUser"));
            if (ActiveUser == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            Wrapper.User = ActiveUser;
            Wrapper.AllServices = _context.Services.Include(s => s.ServiceType).Where(s => s.CarId == Wrapper.Car.CarId).ToList();
            string url = $"https://vpic.nhtsa.dot.gov/api/vehicles/decodevin/{Wrapper.Car.VIN}?format=json&modelyear={Wrapper.Car.Year}";
            Wrapper.CarDetails = url;
            return View("CarDetails", Wrapper);
        }
    }
}