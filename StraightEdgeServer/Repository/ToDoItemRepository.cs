using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ToDoItemRepository : RepositoryBase<ToDoItem>, IToDoItemRepository
    {
        public ToDoItemRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }
        public void CreateToDoItem(Guid taskId, ToDoItem toDoItem)
        {
            toDoItem.TaskId = taskId;
            Create(toDoItem);
        }

        public void DeleteToDoItem(ToDoItem toDoItem) => Delete(toDoItem);

        public async Task<IEnumerable<ToDoItem>> GetAllToDoItemsAsync(Guid taskId, bool trackChanges) =>
            await FindByCondition(t => t.TaskId.Equals(taskId), trackChanges)
            .ToListAsync();

        public async Task<ToDoItem> GetToDoItemAsync(Guid taskId, Guid toDoItemId, bool trackChanges) =>
            await FindByCondition(t => t.TaskId.Equals(taskId) && t.Id.Equals(toDoItemId), trackChanges)
            .SingleOrDefaultAsync();
    }
}
