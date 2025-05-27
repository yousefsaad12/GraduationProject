

using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Dtos
{
    public class CreateDoctorRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string name { set; get; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string specialization { set; get; }

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string email { set; get; }

        [StringLength(20)]
        public string? phoneNumber { set; get; }

        [StringLength(50)]
        public string? country { set; get; }

        [StringLength(200)]
        public string? location { set; get; }
    }
}