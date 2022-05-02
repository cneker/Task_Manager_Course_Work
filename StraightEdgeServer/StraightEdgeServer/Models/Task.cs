using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StraightEdgeServer.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //late make ExecutorId and User object as Executor
        public string ExecutorEmail { get; set; }
        [Column(TypeName = "date")]
        public DateTime DeadLine { get; set; }
        public bool IsCompleted { get; set; }
        public bool NotificationEnabled { get; set; }


        [ForeignKey("Email")]
        public string UserEmail { get; set; }
        //public User User { get; set; }

        public List<ToDo> ToDoList { get; set; }
    }
}
