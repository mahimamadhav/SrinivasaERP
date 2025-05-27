using System.ComponentModel.DataAnnotations;

namespace SrinivasaERP.Models
{
    public class Register
    {

        [Required, MinLength(2), MaxLength(25)]
        public string? Name { get; set; }

        [Required, MinLength(1), MaxLength(25)]
        public string? Surname { get; set; }

        [Required, StringLength(13)]
        public string? Phone { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required, RegularExpression("^[A-Z]{3}[0-9]{4}$")]
        [Key]
        public string? UserID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,12}$",
         ErrorMessage = "Invalid Password Format")]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
    }
}
