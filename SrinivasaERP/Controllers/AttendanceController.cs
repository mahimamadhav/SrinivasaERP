using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        public ActionResult Index()
        {
            var viewModel = new AttendanceViewModel();
            string attendanceConnStr = _configuration.GetConnectionString("AttendanceDBConnection");

            try
            {
                using (SqlConnection conn = new SqlConnection(attendanceConnStr))
                {
                    conn.Open();

                    var today = DateTime.Today;
                    var yesterday = today.AddDays(-1);

                    // ✅ Use local variables here to fix the 'ref/out' error
                    DateTime todayDate;
                    string? todayInTime, todayOutTime, todayOutLocation;

                    LoadAttendanceData(conn, today, out todayDate, out todayInTime, out todayOutTime, out todayOutLocation);

                    viewModel.TodayDate = todayDate;
                    viewModel.TodayInTime = todayInTime;
                    viewModel.TodayOutTime = todayOutTime;
                    viewModel.TodayOutLocation = todayOutLocation;

                    // ✅ Do the same for yesterday
                    DateTime yesterdayDate;
                    string? yesterdayInTime, yesterdayOutTime, yesterdayOutLocation;

                    LoadAttendanceData(conn, yesterday, out yesterdayDate, out yesterdayInTime, out yesterdayOutTime, out yesterdayOutLocation);

                    viewModel.YesterdayDate = yesterdayDate;
                    viewModel.YesterdayInTime = yesterdayInTime;
                    viewModel.YesterdayOutTime = yesterdayOutTime;
                    viewModel.YesterdayOutLocation = yesterdayOutLocation;

                    // Load shift details
                    viewModel.ShiftDetails = LoadShiftDetails(conn);

                    // Dummy chart data
                    viewModel.MonthSummary = new MonthSummary
                    {
                        SummaryDate = DateTime.Today,
                        MonthName = DateTime.Today.ToString("MMMM yyyy")
                    };

                    viewModel.Months = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun" };
                    viewModel.PresentDays = new List<int> { 20, 21, 19, 22, 20, 18 };
                    viewModel.AbsentDays = new List<int> { 1, 2, 2, 1, 2, 3 };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dashboard data: {ex.Message}");
                ViewBag.ErrorMessage = "Unable to load attendance data.";
            }


            return View(viewModel); 
        }
        private void LoadAttendanceData(SqlConnection conn, DateTime date, out DateTime outDate, out string? inTime, out string? outTime, out string? outLocation)
        {
            outDate = date;
            inTime = outTime = outLocation = null;

            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 InTime, OutTime, OutLocation FROM AttendanceLogs WHERE Date = @Date", conn))
            {
                cmd.Parameters.AddWithValue("@Date", date);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        inTime = reader["InTime"] != DBNull.Value ? reader["InTime"].ToString() : null;
                        outTime = reader["OutTime"] != DBNull.Value ? reader["OutTime"].ToString() : null;
                        outLocation = reader["OutLocation"] != DBNull.Value ? reader["OutLocation"].ToString() : null;
                    }
                }
            }
        }

        private List<ShiftDetail> LoadShiftDetails(SqlConnection conn)
        {
            var shiftDetails = new List<ShiftDetail>();

            using (SqlCommand cmd = new SqlCommand("SELECT TOP 5 Date, DayType, InTime, OutTime, ShiftLabel FROM ShiftDetails ORDER BY Date DESC", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    shiftDetails.Add(new ShiftDetail
                    {
                        Date = reader["Date"] != DBNull.Value ? Convert.ToDateTime(reader["Date"]) : DateTime.MinValue,
                        DayType = reader["DayType"]?.ToString(),
                        InTime = reader["InTime"]?.ToString(),
                        OutTime = reader["OutTime"]?.ToString(),
                        ShiftLabel = reader["ShiftLabel"]?.ToString()
                    });
                }
            }

            return shiftDetails;
        }
    }
}
