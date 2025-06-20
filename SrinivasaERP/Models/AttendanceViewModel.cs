using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SrinivasaERP.Models
{
    public class AttendanceViewModel
    {
        public DateTime TodayDate { get; set; }
        public string? TodayInTime { get; set; }
        public string? TodayOutTime { get; set; }
        public string? TodayOutLocation { get; set; }

        public DateTime YesterdayDate { get; set; }
        public string? YesterdayInTime { get; set; }
        public string? YesterdayOutTime { get; set; }
        public string? YesterdayOutLocation { get; set; }

        public DateTime TomorrowDate { get; set; }
        public DateTime MonthSummaryDate { get; set; }

        public List<ShiftDetail> ShiftDetails { get; set; } = new List<ShiftDetail>();
        public MonthSummary MonthSummary { get; set; } = new MonthSummary();
    }

    [Table("ShiftDetails")]
    public class ShiftDetail
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public string? DayType { get; set; }
        public string? InTime { get; set; }
        public string? OutTime { get; set; }
        public string? ShiftLabel { get; set; }
    }

    [Table("MonthSummaries")]
    public class MonthSummary
    {
        [Key]
        public int Id { get; set; }

        public DateTime SummaryDate { get; set; }
        public string? MonthName { get; set; }
    }
}
