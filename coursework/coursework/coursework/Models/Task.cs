using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace coursework.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //late make ExecutorId and User object as Executor
        public string ExecutorEmail { get; set; }
        public DateTime DeadLine { get; set; }
        public bool IsCompleted { get; set; }
        public bool NotificationEnabled { get; set; }

        public string UserEmail { get; set; }
        //public User User { get; set; }

        public List<ToDo> ToDoList { get; set; }

        public int CountOfCompletedToDo { get; set; }
        public string TaskOwner { get; set; }
    }
}
