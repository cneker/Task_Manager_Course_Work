using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Task
    {
        [Column("TaskId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Task name is required!")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Task's deadline is required!")]
        public DateTime DeadLine { get; set; }
        public bool IsCompleted { get; set; }
        public bool NotificationEnabled { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ExecutorId { get; set; }
        public User Executor { get; set; }

        public List<ToDoItem> ToDoItems { get; set; }
    }
}
