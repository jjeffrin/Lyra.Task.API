using Lyra.TaskService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Lyra.TaskService.API.DataAccess
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options): base(options)
        {
            
        }

        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<WorkItemStatusCode> WorkItemsStatusCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItem>()
                .HasOne(x => x.WorkItemStatus)
                .WithMany(x => x.WorkItems)
                .HasForeignKey(s => s.StatusCode);
        }
    }
}
