using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<ToDoList>> Post(ToDoList todo)
        {
            if (todo is null)
                return BadRequest();
            await db.ToDoLists.AddAsync(todo);
            await db.SaveChangesAsync();

            return Ok(todo);
        }


    }
}
