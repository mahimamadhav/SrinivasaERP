using System.ComponentModel.DataAnnotations;

namespace SrinivasaERP.Models
{
    public class ForgotModel
    {
        [Required]
        [Display(Name = "Employee ID or Email")]
        public string EmployeeIDOrEmail { get; set; }
    }
}
