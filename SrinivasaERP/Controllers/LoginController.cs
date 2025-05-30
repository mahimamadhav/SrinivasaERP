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
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var input = model.EmployeeIDOrEmail?.Trim();
            var password = model.Password?.Trim();

            Console.WriteLine($"Input: {input}, {password}");

            var user = await _context.Registers
                .FirstOrDefaultAsync(u =>
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
        public IActionResult dashbord()
        {
            ViewBag.UserName = "Rajiv Sharma"; // Replace with dynamic user data
            return View();
        }
    }
}

