

using System.ComponentModel.DataAnnotations;

namespace Api.Dto
{
    public class LoginReq
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(50, ErrorMessage = "Password must but not less than 12 char")]
        public string passWord { get; set; }
    }
}