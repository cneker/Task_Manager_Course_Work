using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class UserForRegistrationDto
    {
        [Required(ErrorMessage = "User email is required!")]
        public string Email { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "User password is required!")]
        public string Password { get; set; }
    }
}
