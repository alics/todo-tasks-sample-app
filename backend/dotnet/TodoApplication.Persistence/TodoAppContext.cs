using Microsoft.EntityFrameworkCore;

namespace TodoApplication.Persistence
{
    
    public class TodoAppContext(DbContextOptions<TodoAppContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
