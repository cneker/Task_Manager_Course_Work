using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "User email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "User password is required")]
        public string Password { get; set; }
    }
}
