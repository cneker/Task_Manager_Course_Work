using System;

namespace Entities.DTOs
{
    public class ToDoItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
