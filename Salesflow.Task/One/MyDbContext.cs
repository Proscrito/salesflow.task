using Microsoft.EntityFrameworkCore;

namespace Salesflow.Task.One
{
    public class MyDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
        }
    }
}