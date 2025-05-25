using Microsoft.AspNetCore.Mvc;
using SrinivasaERP.Models;
using System.Reflection;

namespace SrinivasaERP.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel Model)
        {
            if (!ModelState.IsValid)
            {
                return View("login", Model);
            }
            Console.WriteLine(Model.EmployeeIDOrEmail);
            if ((Model.EmployeeIDOrEmail == "admin" || Model.EmployeeIDOrEmail == "admin@example.com") && Model.Password == "admin123")
            {
                TempData["Success"] = "Login successful!";
                //return RedirectToAction("Dashboard", "Login");
                return View("login", Model);
            }
            else
            {
                TempData["Error"] = "Invalid EmployeeID or Password.";
                return View("login", Model);
            }
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
            
        public IActionResult ForgotPassword(ForgotModel Model)
        {
            Console.WriteLine(Model.EmployeeIDOrEmail);
            if (!ModelState.IsValid)
            {
                return View("forgotpassword", Model);
            }
            if (Model.EmployeeIDOrEmail == "admin"|| Model.EmployeeIDOrEmail == "admin@example.com")
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
    }
}

