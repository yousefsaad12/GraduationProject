

using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Dtos
{
    public class CreateDoctorRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { set; get; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Specialization { set; get; }

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Email { set; get; }

        [StringLength(20)]
        public string? PhoneNumber { set; get; }

        [StringLength(50)]
        public string? Country { set; get; }

        [StringLength(200)]
        public string? Location { set; get; }
    }
}