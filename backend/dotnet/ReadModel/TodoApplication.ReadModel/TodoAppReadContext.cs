using Microsoft.EntityFrameworkCore;
using TodoApplication.ReadModel.Models;

namespace TodoApplication.ReadModel;

public partial class TodoAppReadContext : DbContext
{
    public TodoAppReadContext()
    {
    }

    public TodoAppReadContext(DbContextOptions<TodoAppReadContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TaskHistory> TaskHistories { get; set; }

    public virtual DbSet<TodoTask> TodoTasks { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Name=ConnectionStrings:TodoAppConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskHistory>(entity =>
        {
            entity.HasIndex(e => e.TaskId, "IX_TaskHistories_TaskId");

            entity.Property(e => e.DateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskHistories).HasForeignKey(d => d.TaskId);
        });

        modelBuilder.Entity<TodoTask>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Deadline).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}