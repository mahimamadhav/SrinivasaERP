using System.ComponentModel.DataAnnotations;
namespace SrinivasaERP.Models
{
    public class EmployeeProfileViewModel
    {
        [Required, MinLength(2), MaxLength(25)]
        public string FirstName { get; set; }

        [Required, MinLength(2), MaxLength(15)]
        public string MiddleName { get; set; }

        [Required, MinLength(2), MaxLength(25)]
        public string Surname { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required, MinLength(2), MaxLength(25)]
        public string DisplayName { get; set; }

        [Required, RegularExpression("^[1-9][0-9]$", ErrorMessage = "Age must be a number between 10 and 99.")]
        public string Age { get; set; }

        [RegularExpression(@"^\+\d{2}\s\d{10}$", ErrorMessage = "Phone number must be in the format +91 9876543210")]
        public string Phone { get; set; }

    }
}
