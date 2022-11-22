using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class TaskForCreationDto
    {
        [Required(ErrorMessage = "Task name is required!")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Task deadline is required!")]
        public DateTime DeadLine { get; set; }
        public bool IsCompleted { get; set; }
        public bool NotificationEnabled { get; set; }

        [Required(ErrorMessage = "Creator email is required!")]
        public string UserEmail { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Executor email is required!")]
        public string ExecutorEmail { get; set; }

        public List<ToDoItem> ToDoItems { get; set; }
    }
}
