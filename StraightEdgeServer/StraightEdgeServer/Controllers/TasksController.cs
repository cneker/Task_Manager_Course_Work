using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StraightEdgeServer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = StraightEdgeServer.Models.Task;

namespace StraightEdgeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly ILogger _logger;

        public TasksController(ApplicationContext context, ILogger<TasksController> logger)
        {
            _db = context;
            _logger = logger;
        }

        //api/tasks/get
        [Route("get")]
        [HttpGet]
        public async Task<ActionResult<Task>> Get(int id)
        {
            _logger.LogInformation("Getting task {id}", id);
            var task = await _db.Tasks
                .Include(t => t.ToDoList)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (task is null)
            {
                _logger.LogWarning("Get{id} NOT FOUND", id);
                return NotFound();
            }
            return Ok(task);
        }

        //api/tasks/create
        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<Task>> Post(Task task)
        {
            _logger.LogInformation("Creating task");
            if (task is null)
            {
                _logger.LogWarning("Post(task) BAD REQUEST");
                return BadRequest();
            }
            await _db.Tasks.AddAsync(task);
            await _db.SaveChangesAsync();

            task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == task.Id);

            return Ok(task);
        }

        //api/tasks/update
        [Route("update")]
        [HttpPut]
        public async Task<ActionResult<Task>> Put(Task task)
        {
            _logger.LogInformation("Updating task");
            if (task is null)
            {
                _logger.LogWarning("Put(task) BAD REQUEST");
                return BadRequest();
            }

            _db.Tasks.Update(task);
            await _db.SaveChangesAsync();

            return Ok(task);
        }

        //api/tasks/delete
        [Route("delete")]
        [HttpDelete]
        public async Task<ActionResult<Task>> Delete(int id)
        {
            _logger.LogInformation("Deleting task {id}", id);
            var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task is null)
            {
                _logger.LogWarning("Delete({id}) NOT FOUND", id);
                return NotFound();
            }

            task = _db.Tasks.Remove(task).Entity;
            await _db.SaveChangesAsync();
            return Ok(task);
        }

        //api/tasks/user_tasks
        [Route("user_tasks")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks(string email)
        {
            _logger.LogInformation("Getting user {email} tasks", email);
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            var tasks = await _db.Tasks
                .Include(t => t.ToDoList)
                .Where(t => t.UserEmail == user.Email || t.ExecutorEmail == user.Email)
                .ToListAsync();
            return Ok(tasks);
        }
    }
}
