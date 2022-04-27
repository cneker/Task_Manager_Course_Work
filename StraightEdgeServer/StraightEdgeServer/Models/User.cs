using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StraightEdgeServer.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
