using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApplication.Domain.TodoTasks;

namespace TodoApplication.Persistence.TodoTasks;

public class TodoTaskMapping : IEntityTypeConfiguration<TodoTask>
{
    public void Configure(EntityTypeBuilder<TodoTask> builder)
    {
        builder.ToTable("TodoTasks");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnType(SqlDbType.BigInt.ToString()).IsRequired().ValueGeneratedNever();
        builder.Property(p => p.Title).HasColumnType("nvarchar(500)").IsRequired();
        builder.Property(p => p.Status).HasColumnType(SqlDbType.TinyInt.ToString()).IsRequired();
        builder.Property(p => p.CreationDate).HasColumnType(SqlDbType.DateTime.ToString()).IsRequired();
        builder.Property(p => p.Deadline).HasColumnType(SqlDbType.DateTime.ToString()).IsRequired();

        builder.OwnsMany(p => p.TaskHistories, n =>
        {
            n.ToTable("TaskHistories");
            n.HasKey("Id");

            n.WithOwner().HasForeignKey(p => p.TaskId).Metadata.DeleteBehavior = DeleteBehavior.Cascade;

            n.Property(p => p.TaskStatus).HasColumnType(SqlDbType.TinyInt.ToString()).IsRequired();
            n.Property(p => p.DateTime).HasColumnType(SqlDbType.DateTime.ToString()).IsRequired();
        });
    }
}