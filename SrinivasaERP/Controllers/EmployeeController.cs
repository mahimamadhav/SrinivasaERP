using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SrinivasaERP.Data;
using SrinivasaERP.Models;
using System;

namespace SrinivasaERP.Controllers
{
    public class EmployeeController : Controller
    {
        
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> CurrentAddress()
        {
            var userId = GetCurrentUserId();
            var address = await _context.CurrentAddresses
                .FirstOrDefaultAsync(a => a.UserId == userId)
                ?? new CurrentAddress();

            ViewBag.Cities = GetCities();
            ViewBag.States = GetStates();
            return View(address);
        }

        private string GetCurrentUserId()
        {
            // Replace with your actual user identification logic
            return User.Identity.Name ?? "default-user-id";
        }

        private List<string> GetCities() => new() { "Delhi", "Mumbai", "Bangalore", "Hyderabad","Guntur","Nellore" };
        private List<string> GetStates() => new() { "Arunachal Pradesh", "Andhra Pradesh", "Bihar", "Assam", "Chhattisgarh", "Goa", "Gujarat", "Haryana", "Himachal Pradesh", "Jammu", "Jharkhand", "Karnataka", "Kerala", "Maharashtra", "Manipur", "Meghalaya", "Mizoram", "Nagaland", "Odisha", "Punjab", "Rajasthan", "Sikkim", "Tamil Nadu", "Telangana", "Tripura", "Uttarakhand", "Uttar Pradesh", "West Bengal", "Madhya Pradesh" };
    }
}
