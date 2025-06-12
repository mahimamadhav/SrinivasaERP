using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SrinivasaERP.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 25 characters")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Name can contain only alphabets")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "Surname must be between 1 and 25 characters")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Surname can contain only alphabets")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"\+91\d{10}", ErrorMessage = "Phone number must start with +91 and be followed by 10 digits.")]
        public string? Phone { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [Key]
        [Required(ErrorMessage = "User ID is required")]
        [RegularExpression(@"^(?!.*0000$)[A-Z]{3}\d{4}$", ErrorMessage = "Code must be 3 uppercase letters followed by 4 digits and must not end with 0000.")]
        public string? UserID { get; set; }


        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$", ErrorMessage = "Password must include uppercase, lowercase, number, and special character.")]
        public string? Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
    }
}
