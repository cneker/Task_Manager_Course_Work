using System.Collections.Generic;

namespace StraightEdgeServer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
