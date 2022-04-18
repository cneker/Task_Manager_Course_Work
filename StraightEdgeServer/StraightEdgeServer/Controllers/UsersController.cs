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

        //api/users/login
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<User>> Log(User user)
        {
            if (user is null)
                return BadRequest();
            var match = await db.Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
            if (match is null)
                return NotFound();
            return Ok(match);
        }

        //api/users/registration
        [Route("registration")]
        [HttpPost]
        public async Task<ActionResult<User>> Reg(User user)
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

        //api/users/get
        [Route("get")]
        [HttpGet]
        public async Task<ActionResult<User>> Get(int id)
        {
            if (id == 0)
                return BadRequest();
            var match = await db.Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (match is null)
                return NotFound();
            return Ok(match);
        }
    }
}
