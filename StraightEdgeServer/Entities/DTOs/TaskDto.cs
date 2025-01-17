﻿using Entities.Models;
using System;

namespace Entities.DTOs
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DeadLine { get; set; }
        public bool IsCompleted { get; set; }
        public bool NotificationEnabled { get; set; }

        public string UserEmail { get; set; }
        public User User { get; set; }

        public string ExecutorEmail { get; set; }
    }
}
