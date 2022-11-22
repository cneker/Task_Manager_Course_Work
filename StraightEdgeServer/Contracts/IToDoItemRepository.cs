using Entities.Models;

namespace Contracts
{
    public interface IToDoItemRepository
    {
        Task<IEnumerable<ToDoItem>> GetAllToDoItemsAsync(Guid taskId, bool trackChanges);
        Task<ToDoItem> GetToDoItemAsync(Guid taskId, Guid toDoItemId, bool trackChanges);
        void CreateToDoItem(Guid taskId, ToDoItem toDoItem);
        void DeleteToDoItem(ToDoItem toDoItem);
    }
}
