using System.ComponentModel.DataAnnotations;

namespace SrinivasaERP.Models
{
    public class ResetPasswordModel
    {
        public string? Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
