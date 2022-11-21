using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ToDoItem
    {
        [Column("ToDoItemId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Item name is required!")]
        public string Name { get; set; }
        public bool IsCompleted { get; set; }

        public Guid TaskId { get; set; }
        public Task Task { get; set; }
    }
}
