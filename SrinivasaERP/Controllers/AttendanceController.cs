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

        
        public ActionResult Index(int days = 7)
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

                 
                    LoadAttendanceData(conn, today, out var todayDate, out var todayInTime, out var todayOutTime, out var todayOutLocation);
                    viewModel.TodayDate = todayDate;
                    viewModel.TodayInTime = todayInTime;
                    viewModel.TodayOutTime = todayOutTime;
                    viewModel.TodayOutLocation = todayOutLocation;

                    LoadAttendanceData(conn, yesterday, out var yesterdayDate, out var yesterdayInTime, out var yesterdayOutTime, out var yesterdayOutLocation);
                    viewModel.YesterdayDate = yesterdayDate;
                    viewModel.YesterdayInTime = yesterdayInTime;
                    viewModel.YesterdayOutTime = yesterdayOutTime;
                    viewModel.YesterdayOutLocation = yesterdayOutLocation;

                    
                    viewModel.ShiftDetails = LoadShiftDetails(conn);

                    viewModel.MonthSummary = new MonthSummary
                    {
                        SummaryDate = DateTime.Today,
                        MonthName = DateTime.Today.ToString("MMMM yyyy")
                    };

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
                        viewModel.PresentDays.Add(rnd.Next(18, 23)); 
                        viewModel.AbsentDays.Add(rnd.Next(0, 3));    
                    }
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