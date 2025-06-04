using System;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SrinivasaERP.Data;
using SrinivasaERP.Models;

namespace SrinivasaERP.Controllers
{
    public class HomeController : Controller
    {


        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CheckUserExists(string userID, string email)
        {
            bool isAvailable = true;

            if (!string.IsNullOrEmpty(userID))
            {
                isAvailable = _context.Registers.Any(u => u.UserID == userID);
            }
            else if (!string.IsNullOrEmpty(email))
            {
                isAvailable = _context.Registers.Any(u => u.Email == email);
            }

            return Json(isAvailable);
        }


        [HttpPost]
        public async Task<IActionResult> RegisterAsync(Register model)
        {
            Console.WriteLine(model);
            Console.WriteLine($"Email: {model.Email}, Password: {model.ConfirmPassword}");
            string json = JsonSerializer.Serialize(model);
            Console.WriteLine(json);

            if (ModelState.IsValid)
            {
                _context.Registers.Add(model);
                _context.SaveChanges();
                
                return RedirectToAction("Login", "Login");

            }

            return View(model);
        }

    }
}
