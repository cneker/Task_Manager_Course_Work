using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
