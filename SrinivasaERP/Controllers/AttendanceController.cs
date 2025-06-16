using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SrinivasaERP.Models;

namespace SrinivasaERP.Controllers
{
    public class AttendanceController : Controller
    {
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
    }
}
