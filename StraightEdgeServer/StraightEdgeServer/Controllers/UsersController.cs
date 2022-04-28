using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StraightEdgeServer.Models;

namespace StraightEdgeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly ILogger _logger;

        public UsersController(ApplicationContext context, ILogger<UsersController> logger)
        {
            _db = context;
            _logger = logger;
        }

        //api/users/login
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<User>> Log(User user)
        {
            _logger.LogInformation("Authentificate user");
            if (user is null)
            {
                _logger.LogWarning("Log(user) BAD REQUEST");
                return BadRequest();
            }
            var match = await _db.Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
            if (match is null)
            {
                _logger.LogWarning($"Log(user) {user.Email} NOT FOUND");
                return NotFound();
            }
            return Ok(match);
        }

        //api/users/registration
        [Route("registration")]
        [HttpPost]
        public async Task<ActionResult<User>> Reg(User user)
        {
            _logger.LogInformation("Registrating user");
            if (user is null)
            {
                _logger.LogWarning("Reg(user) BAD REQUEST");
                return BadRequest();
            }
            var match = await _db.Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Email == user.Email);
            if (match != null)
            {
                _logger.LogWarning("Reg(user) {email} EXISTS", user.Email);
                return BadRequest();
            }
            await _db.AddAsync(user);
            await _db.SaveChangesAsync();

                match = await _db.Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Email == user.Email);
            return Ok(match);
        }
    }
}
