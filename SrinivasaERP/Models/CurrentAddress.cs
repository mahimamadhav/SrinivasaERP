using System.ComponentModel.DataAnnotations;

namespace SrinivasaERP.Models
{
    public class CurrentAddress
    {
        [Key]
        public int Id {  get; set; }    
        
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-Za-z0-9\s,/-]*$", ErrorMessage = "Invalid characters.")]
        [Display(Name = "Flat Number/House Number/Apartment Name")]
        public string? FlatHouseApartment { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-Za-z0-9\s]*$", ErrorMessage = "Only alphabets, numbers and spaces allowed.")]
        [Display(Name = "Area/Colony/Street")]
        public string? AreaColonyStreet { get; set; }

        [StringLength(40)]
        [RegularExpression(@"^[A-Za-z0-9\s]*$", ErrorMessage = "Only alphabets, numbers and spaces allowed.")]
        public string? Landmark { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? State { get; set; }

        [Required]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "PIN Code must be exactly 6 digits.")]
        [Display(Name = "PIN Code")]
        public string? PinCode { get; set; }

        public bool IsApproved { get; set; } = false; // Admin approval
        public string? UserId { get; internal set; }
    }
}
