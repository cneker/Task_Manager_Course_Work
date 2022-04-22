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
        public string Executor { get; set; }
        //late change type to DateAndTime 
        [Column(TypeName = "date")]
        public DateTime DeadLine { get; set; }
        [DefaultValue("null")]
        public bool? IsCompleted { get; set; }

        public int UserId { get; set; }
        //public User User { get; set; }

        public List<ToDoList> ToDoList { get; set; }
    }
}
