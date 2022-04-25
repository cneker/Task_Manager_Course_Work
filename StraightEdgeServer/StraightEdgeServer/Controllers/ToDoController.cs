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
    public class ToDoController : ControllerBase
    {
        private ApplicationContext db;

        public ToDoController(ApplicationContext context)
        {
            db = context;
        }

        //api/todo/create
        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<ToDo>> Post(ToDo todo)
        {
            if (todo is null)
                return BadRequest();
            await db.ToDoLists.AddAsync(todo);
            await db.SaveChangesAsync();

            return Ok(todo);
        }

        //api/todo/delete
        [Route("delete")]
        [HttpDelete]
        public async Task<ActionResult<ToDo>> Delete(int id)
        {
            var toDo = await db.ToDoLists.FirstAsync(t => t.Id == id);
            if (toDo == null)
                return NotFound();
            db.ToDoLists.Remove(toDo);
            await db.SaveChangesAsync();
            return Ok(toDo);
        }
    }
}
