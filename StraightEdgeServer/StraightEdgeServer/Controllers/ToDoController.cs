using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StraightEdgeServer.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace StraightEdgeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly ILogger _logger;

        public ToDoController(ApplicationContext context, ILogger<ToDoController> logger)
        {
            _db = context;
            _logger = logger;
        }

        //api/todo/delete
        [Route("delete")]
        [HttpDelete]
        public async Task<ActionResult<ToDo>> Delete(int id)
        {
            _logger.LogInformation("Deleting to do item {id}", id);
            var toDo = await _db.ToDoLists.FirstAsync(t => t.Id == id);
            if (toDo == null)
            {
                _logger.LogWarning("Delete({id}) NOT FOUND", id);
                return NotFound();
            }

            toDo = _db.ToDoLists.Remove(toDo).Entity;
            await _db.SaveChangesAsync();
            return Ok(toDo);
        }
    }
}
