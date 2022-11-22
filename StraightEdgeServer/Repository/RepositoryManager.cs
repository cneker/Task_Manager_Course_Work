using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ITaskRepository _taskRepository;
        private IToDoItemRepository _toDoItemRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public ITaskRepository TaskRepository
        {
            get
            {
                if (_taskRepository == null)
                    _taskRepository = new TaskRepository(_repositoryContext);
                return _taskRepository;
            }
        }

        public IToDoItemRepository ToDoItemRepository
        {
            get
            {
                if (_toDoItemRepository == null)
                    _toDoItemRepository = new ToDoItemRepository(_repositoryContext);
                return _toDoItemRepository;
            }
        }

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
