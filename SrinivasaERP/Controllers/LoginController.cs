using Microsoft.AspNetCore.Mvc;
using SrinivasaERP.Models;
using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using SrinivasaERP.Data;
using Microsoft.AspNetCore.Http;

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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var input = model.EmployeeIDOrEmail?.Trim();
            var password = model.Password?.Trim();

            var user = _context.Registers
                .FirstOrDefault(u => (u.UserID == input || u.Email == input) && u.Password == password);

            if (user!= null)
            {
                HttpContext.Session.SetString("UserName", user.Name );
                HttpContext.Session.SetString("UserEmail", user.Email);
                return RedirectToAction("Dashbord");
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
        public IActionResult ForgotPassword(ForgotModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var input = model.EmployeeIDOrEmail?.Trim();
            var user = _context.Registers.FirstOrDefault(u => u.UserID == input || u.Email == input);

            if (user != null)
            {
                // Generate token
                var token = Guid.NewGuid().ToString();
                user.ResetToken = token;
                user.ResetTokenExpiry = DateTime.UtcNow.AddHours(1);
                _context.SaveChanges();

                // Build reset link
                var resetLink = Url.Action("ResetPassword", "Login", new { token = token }, Request.Scheme);

                // Send email
                SendResetEmail(user.Email, resetLink);

                TempData["Success"] = "Password reset link has been sent to your email.";
                return View();
            }

            TempData["Error"] = "Invalid EmployeeID or Email.";
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            var user = _context.Registers.FirstOrDefault(u =>
                u.ResetToken == token && u.ResetTokenExpiry > DateTime.UtcNow);

            if (user == null)
                return View("InvalidToken");

            return View(new ResetPasswordModel { Token = token });
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            var user = _context.Registers.FirstOrDefault(u =>
                u.ResetToken == model.Token && u.ResetTokenExpiry > DateTime.UtcNow);

            if (user == null)
                return View("InvalidToken");

            user.Password = model.NewPassword; 
            user.ResetToken = null;
            user.ResetTokenExpiry = null;
            _context.SaveChanges();

            return View("ResetPasswordConfirmation");
        }

        private void SendResetEmail(string toEmail, string? resetLink)
        {
            var fromAddress = new MailAddress("sambasivaraokonakala90@gmail.com", "SrinivasaERP");
            var toAddress = new MailAddress(toEmail);
            const string subject = "Password Reset Link";
            string body = $"<p>Please click the link below to reset your password:</p><p><a href='{resetLink}'>Reset Password</a></p>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("sambasivaraokonakala90@gmail.com", "xope zgtz nslq hcbr")
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }


        public IActionResult Dashbord()
        {
            
            var userName = HttpContext.Session.GetString("UserEmail");
            ViewBag.Name = userName;
            return View();
        }

        public IActionResult Profile()
        {
            var userName = HttpContext.Session.GetString("UserName");
            ViewBag.Name = userName;
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // or sign out logic
            return RedirectToAction("Login", "Login");
        }

    }
}
