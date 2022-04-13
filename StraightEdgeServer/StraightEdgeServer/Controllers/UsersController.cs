using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using StraightEdgeServer.Models;

namespace StraightEdgeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //"authorization"
    public class UsersController : ControllerBase
    {
        private ApplicationContext db;

        public UsersController(ApplicationContext context)
        {
            db = context;
        }

        //api/users/get
        [Route("get")]
        [HttpPost]
        public async Task<ActionResult<User>> Get(User user)
        {
            if (user is null)
                return BadRequest();
            var match = await db.Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Id == user.Id);
            if (match is null)
                return NotFound();
            return Ok(match);
        }

        //api/users/create
        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            if (user is null)
                return BadRequest();
            var match = await db.Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Id == user.Id);
            if (match is not null)
                return BadRequest();
            await db.AddAsync(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}
