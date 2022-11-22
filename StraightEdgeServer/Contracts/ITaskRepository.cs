using Task = Entities.Models.Task;

namespace Contracts
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Task>> GetAllTasksAsync(bool trackChanges);
        Task<Task> GetTaskAsync(Guid taskId, bool trackChanges);
        void CreateTask(Task task);
        void DeleteTask(Task task);
    }
}
