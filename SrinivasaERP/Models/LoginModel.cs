using System.ComponentModel.DataAnnotations;

namespace SrinivasaERP.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Employee ID or Email")]
        public string EmployeeIDOrEmail { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
