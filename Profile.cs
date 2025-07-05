using Microsoft.AspNetCore.Mvc;
using SrinivasaERP.Models;

namespace SrinivasaERP.Controllers
{
    public class Profile : Controller
    {
        public IActionResult PersonalDetails()
        {
            var model = new EmployeeProfileViewModel
            {
                FirstName="",
                MiddleName = "",
                Surname = "",
                Gender = "",
                DisplayName = "",
                Age = "",
                Phone = "",
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult ProfileM(EmployeeProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Process / Save data here

            return RedirectToAction("Success");
        }
    }
}
