using System;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Login()
        {
            return View();
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
                // Save to DB
                _context.Registers.Add(model);

                  await _context.SaveChangesAsync();

                return RedirectToAction("Login","Login");
            }

            return View(model);
        }

    }
}
