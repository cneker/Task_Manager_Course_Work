using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StraightEdgeServer.Models;
using Task = StraightEdgeServer.Models.Task;

namespace StraightEdgeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ApplicationContext db;

        public TasksController(ApplicationContext context)
        {
            db = context;
        }

        //api/tasks/get
        [Route("get")]
        [HttpGet]
        public async Task<ActionResult<Task>> Get(int id)
        {
            var task = await db.Tasks
                .Include(t => t.ToDoList)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (task is null)
                return NotFound();
            return Ok(task);
        }

        //api/tasks/create
        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<Task>> Post(Task task)
        {
            if (task is null)
                return BadRequest();
            await db.Tasks.AddAsync(task);
            await db.SaveChangesAsync();

            task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == task.Id);

            return Ok(task);
        }

        //api/tasks/update
        [Route("update")]
        [HttpPut]
        public async Task<ActionResult<Task>> Put(Task task)
        {
            if (task is null)
                return BadRequest();
            //var source = await db.Tasks.Include(t => t.ToDoList).FirstAsync(t => t.Id == task.Id);

            db.Tasks.Update(task);
            await db.SaveChangesAsync();


            return Ok(task);
        }

        //may be remove return Task value
        //api/tasks/delete
        [Route("delete")]
        [HttpDelete]
        public async Task<ActionResult<Task>> Delete(int id)
        {
            var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task is null)
                return NotFound();
            db.Tasks.Remove(task);
            await db.SaveChangesAsync();
            return Ok(task);
        }

        [Route("user_tasks")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks(string email)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            var tasks = await db.Tasks
                .Include(t => t.ToDoList)
                .Where(t => t.UserEmail == user.Email || t.ExecutorEmail == user.Email)
                .ToListAsync();
            return Ok(tasks);
        }
    }
}
