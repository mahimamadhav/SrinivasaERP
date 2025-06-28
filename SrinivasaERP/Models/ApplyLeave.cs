using System.ComponentModel.DataAnnotations;

namespace SrinivasaERP.Models
{
    public class ApplyLeave
    {
        [Key]
        public int LeaveId { get; set; } // Unique identifier for the leave application
        [Required]
       public string LeaveType { get; set; } = string.Empty;
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Status { get; set; } = "Approved"; // Default status is Approved


    }
}
