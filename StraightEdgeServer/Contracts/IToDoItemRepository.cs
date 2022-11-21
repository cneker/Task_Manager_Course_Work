using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IToDoItemRepository
    {
        Task<IEnumerable<ToDoItem>> GetAllToDoItemsAsync(bool trackChanges);
        Task<ToDoItem> GeToDoItemsAsync(bool trackChanges);
        void CreateToDoItem(Guid taskId, ToDoItem toDoItem);
        void DeleteToDoItem(ToDoItem toDoItem);
    }
}
