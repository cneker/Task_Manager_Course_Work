using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
