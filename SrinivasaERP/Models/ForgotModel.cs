using System.ComponentModel.DataAnnotations;

namespace SrinivasaERP.Models
{
    public class ForgotModel
    {
        [Required(ErrorMessage = "Please enter Employee ID or Email")]
        [RegularExpression(@"^([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}|(?!.*0000$)[A-Z]{3}\d{4})$",
        ErrorMessage = "Enter a valid Email or Employee ID (e.g. ABC1234 or name@example.com)")]
        [Display(Name = "Employee ID or Email")]
        public string? EmployeeIDOrEmail { get; set; }
    }
}
