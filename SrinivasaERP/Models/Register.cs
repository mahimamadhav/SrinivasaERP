using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SrinivasaERP.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 25 characters")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Name can contain only alphabets")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "Surname must be between 1 and 25 characters")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Surname can contain only alphabets")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Phone must be exactly 13 digits")]
        [RegularExpression("^[0-9]{13}$", ErrorMessage = "Phone must be 13 digits only")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }
        [Key]
        [Required(ErrorMessage = "User ID is required")]
        [RegularExpression("^[A-Z]{3}[0-9]{4}$", ErrorMessage = "User ID must be in the format AAA1234")]
        public string? UserID { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "Password must be 8–12 characters")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&]).{8,12}$", ErrorMessage = "Password must include uppercase, lowercase, digit, and special character")]
        public string? Password { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
    }
}
