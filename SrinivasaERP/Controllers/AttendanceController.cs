using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SrinivasaERP.Data;
using SrinivasaERP.Models;

namespace SrinivasaERP.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly AppDbContext _context;

        public AttendanceController(AppDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {

            var shiftDetails = new List<ShiftDetail>
            {
                new ShiftDetail
                {
                    Date = DateTime.Today,
                    DayType = "Week Off",
                    InTime = null,
                    OutTime = null,
                    ShiftLabel = ""
                },
                new ShiftDetail
                {
                    Date = DateTime.Today.AddDays(1),
                    DayType = "Regular Shift 8:00AM – 6:00PM",
                    InTime = "9:00AM",
                    OutTime = "6:00PM",
                    ShiftLabel = "Change Shift"
                }
            };

            // Month summary data
            var monthSummary = new MonthSummary
            {
                SummaryDate = DateTime.Today,
                MonthName = DateTime.Today.ToString("MMMM, yyyy")
            };

            // Create the ViewModel
            var model = new AttendanceViewModel
            {
                TodayDate = DateTime.Today,
                YesterdayDate = DateTime.Today.AddDays(-1),
                TomorrowDate = DateTime.Today.AddDays(1),
                ShiftDetails = shiftDetails,
                MonthSummary = monthSummary
            };

            return View(model); // This will return Views/Attendance/Index.cshtml
        }

        [HttpGet]
        public ActionResult Leave()
        {
            // You can initialize any data needed for the Leave view here
            return View(); // This will return Views/Attendance/Leave.cshtml
        }



        [HttpPost]
        public ActionResult Leave(ApplyLeave Leave)
        {
            // Validate the model state and save the leave application

            _context.ApplyLeaves.Add(Leave);
            _context.SaveChanges();
            return RedirectToAction("LeaveHistory"); // Redirect to the LeaveHistory action after saving

        }

        public IActionResult LeaveHistory()
        {
            var leaveRequests = _context.ApplyLeaves
                                        .OrderBy(lr => lr.StartDate)
                                        .ToList();
            return View(leaveRequests);
        }
    }
}