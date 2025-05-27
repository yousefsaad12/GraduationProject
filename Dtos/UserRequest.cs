

using System.ComponentModel.DataAnnotations;

namespace Api.Dto
{
    public class UserRequest
    {
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(50, ErrorMessage = "User Name must be less than 50 characters")]
        public string userName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(50, ErrorMessage = "Password must but not less than 12 char")]
        public string password { get; set; }

    }
}