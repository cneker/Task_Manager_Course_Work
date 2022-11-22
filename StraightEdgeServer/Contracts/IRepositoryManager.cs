namespace Contracts
{
    public interface IRepositoryManager
    {
        ITaskRepository TaskRepository { get; }
        IToDoItemRepository ToDoItemRepository { get; }
        Task SaveAsync();
    }
}
