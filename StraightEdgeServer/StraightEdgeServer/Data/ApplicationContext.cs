using Microsoft.EntityFrameworkCore;

namespace StraightEdgeServer.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<ToDo> ToDoLists { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().Property(t => t.IsCompleted).HasDefaultValue(null);
        }
    }
}
