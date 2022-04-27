using System;
using System.Collections.Generic;
using System.Text;

namespace coursework.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
