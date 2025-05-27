using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name should be between 3 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Specialization is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Specialization should be between 3 and 100 characters.")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(150, ErrorMessage = "Email should not exceed 150 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "Phone number should not exceed 20 characters.")]
        public string? PhoneNumber { get; set; }

        [StringLength(50, ErrorMessage = "Country should not exceed 50 characters.")]
        public string? Country { get; set; }

        [StringLength(200, ErrorMessage = "Location should not exceed 200 characters.")]
        public string? Location { get; set; }
    }

}