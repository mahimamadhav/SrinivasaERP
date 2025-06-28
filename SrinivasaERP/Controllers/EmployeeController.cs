using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SrinivasaERP.Data;
using SrinivasaERP.Models;

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

        private List<string> GetCities() => new() { "Delhi", "Mumbai", "Bangalore", "Hyderabad" };
        private List<string> GetStates() => new() { "Maharashtra", "Karnataka", "Delhi", "Telangana" };
    }
}
