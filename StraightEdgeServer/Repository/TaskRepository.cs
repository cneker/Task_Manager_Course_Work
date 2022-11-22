using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Task = Entities.Models.Task;

namespace Repository
{
    public class TaskRepository : RepositoryBase<Task>, ITaskRepository
    {
        public TaskRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }
        public void CreateTask(Task task) => Create(task);

        public void DeleteTask(Task task) => Delete(task);

        public async Task<IEnumerable<Task>> GetAllTasksAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .ToListAsync();

        public async Task<Task> GetTaskAsync(Guid taskId, bool trackChanges) =>
            await FindByCondition(t => t.Id.Equals(taskId), trackChanges)
            .SingleOrDefaultAsync();
    }
}
