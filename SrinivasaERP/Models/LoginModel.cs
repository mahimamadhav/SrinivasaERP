using System.ComponentModel.DataAnnotations;

namespace SrinivasaERP.Models
{
    public class LoginModel
    {
       

        [Required(ErrorMessage = "Please enter Employee ID or Email")]
        [RegularExpression(@"^([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}|(?!.*0000$)[A-Z]{3}\d{4})$",
    ErrorMessage = "Enter a valid Email or Employee ID (e.g. ABC1234 or name@example.com)")]
        public string? EmployeeIDOrEmail { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters")]
        public string? Password { get; set; }
    }
}

