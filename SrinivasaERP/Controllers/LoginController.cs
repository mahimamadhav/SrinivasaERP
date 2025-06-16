using Microsoft.AspNetCore.Mvc;
using SrinivasaERP.Models;
using System.Reflection;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SrinivasaERP.Data;
using System.Linq;


namespace SrinivasaERP.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<LoginController> _logger;

        public LoginController(AppDbContext context, ILogger<LoginController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var input = model.EmployeeIDOrEmail?.Trim();
            var password = model.Password?.Trim();

            Console.WriteLine($"Input: {input}, {password}");
            var user = _context.Registers
                .FirstOrDefault(u =>
                    (u.UserID == input || u.Email == input) &&
                    u.Password == password);

            if (user != null)
            {
                Console.WriteLine("Login successful");
                // TODO: Set session / redirect
                return RedirectToAction("dashbord");
            }
            else
            {
                Console.WriteLine("Login failed: user not found or password mismatch");
            }
            TempData["Error"] = "Invalid EmployeeID or Password.";
            return View(model);
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
            
        public IActionResult ForgotPassword(ForgotModel Model)
        {
           
            if (!ModelState.IsValid)
            {
                return View(Model);
            }

            var input = Model.EmployeeIDOrEmail?.Trim();
            

            Console.WriteLine($"Input: {input}");

            var user = _context.Registers
                .FirstOrDefault(u =>
                    (u.UserID == input || u.Email == input));

            if (user != null)
            {
                TempData["Success"] = " Password reset link has been sent to your email.";
                return View("forgotpassword");
            }
            else
            {
                TempData["Error"] = "Invalid EmployeeID or Password.";
                  return View("forgotpassword");
            }

        }
        public IActionResult dashbord()
        {
            ViewBag.UserName = "Rajiv Sharma"; // Replace with dynamic user data
            return View();
            //return View("Profile");
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}

