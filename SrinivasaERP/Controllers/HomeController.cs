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
        public JsonResult CheckUserID(string userID)
        {
            bool exists = _context.Registers.Any(u => u.UserID == userID);
            return Json(!exists); 
        }

        [HttpPost]
        public JsonResult CheckEmail(string email)
        {
            bool exists = _context.Registers.Any(u => u.Email == email);
            return Json(!exists); 
        }



        [HttpPost]
        public IActionResult Register(Register model)
        {


            if (ModelState.IsValid)
            {
               object value = _context.Registers.Add(model);
                _context.SaveChanges();
                ViewBag.ShowAlert = true;
                return View();

            }

            return View(model);
        }

    }
}
