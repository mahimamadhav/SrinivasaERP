using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SrinivasaERP.Models;

namespace SrinivasaERP.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IConfiguration _configuration;

        public AttendanceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActionResult Index(int days = 7)
        {
            var viewModel = new AttendanceViewModel();

            try
            {
                var today = DateTime.Today;
                var yesterday = today.AddDays(-1);

                // Generate dummy attendance data
                viewModel.TodayDate = today;
                viewModel.TodayInTime = "09:10 AM";
                viewModel.TodayOutTime = "06:15 PM";
                viewModel.TodayOutLocation = "Office";

                viewModel.YesterdayDate = yesterday;
                viewModel.YesterdayInTime = "09:05 AM";
                viewModel.YesterdayOutTime = "06:05 PM";
                viewModel.YesterdayOutLocation = "Office";

                // Dummy shift details
                viewModel.ShiftDetails = LoadDummyShiftDetails();

                viewModel.MonthSummary = new MonthSummary
                {
                    SummaryDate = DateTime.Today,
                    MonthName = DateTime.Today.ToString("MMMM yyyy")
                };

                // Determine how many months to show based on 'days'
                int monthsToShow;
                if (days <= 30)
                    monthsToShow = 1;
                else if (days <= 90)
                    monthsToShow = 3;
                else if (days <= 180)
                    monthsToShow = 6;
                else
                    monthsToShow = 12;

                viewModel.Months = new List<string>();
                viewModel.PresentDays = new List<int>();
                viewModel.AbsentDays = new List<int>();

                var start = DateTime.Today.AddMonths(-monthsToShow + 1);
                var rnd = new Random();

                for (int i = 0; i < monthsToShow; i++)
                {
                    var month = start.AddMonths(i);
                    viewModel.Months.Add(month.ToString("MMM yyyy"));
                    viewModel.PresentDays.Add(rnd.Next(18, 23)); // 18–22 days present
                    viewModel.AbsentDays.Add(rnd.Next(0, 3));    // 0–2 days absent
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dashboard data: {ex.Message}");
                ViewBag.ErrorMessage = "Unable to load attendance data.";
            }

            return View(viewModel);
        
        }

        private List<ShiftDetail> LoadDummyShiftDetails()
        {
            var shiftDetails = new List<ShiftDetail>();
            var today = DateTime.Today;

            for (int i = 0; i < 5; i++)
            {
                shiftDetails.Add(new ShiftDetail
                {
                    Date = today.AddDays(-i),
                    DayType = i % 6 == 0 ? "Holiday" : "Working Day",
                    InTime = "09:00 AM",
                    OutTime = "06:00 PM",
                    ShiftLabel = i % 2 == 0 ? "General Shift" : "Second Shift"
                });
            }

            return shiftDetails;
        }
    }
}
