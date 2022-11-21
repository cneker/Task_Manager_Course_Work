using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Task = Entities.Models.Task;

namespace Contracts
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Task>> GetAllTasksAsync(bool trackChanges);
        Task<Task> GetTaskAsync(bool trackChanges);
        void CreateTask(Task task);
        void DeleteTask(Task task);
    }
}
