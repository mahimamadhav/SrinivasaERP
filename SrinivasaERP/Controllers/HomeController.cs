using System;
using System.Diagnostics;
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
        public IActionResult Register(Register model)
        {
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
