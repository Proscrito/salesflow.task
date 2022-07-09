using Microsoft.EntityFrameworkCore;
using Salesflow.Task.Three.DataAccess.Entity;

namespace Salesflow.Task.Three.DataAccess
{
    public class TasksDbContext : DbContext
    {
        public DbSet<WorkerTask> Tasks { get; set; }

        public TasksDbContext(DbContextOptions<TasksDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkerTaskConfiguration());
        }
    }
}
