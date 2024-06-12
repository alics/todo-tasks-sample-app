using System.Linq.Expressions;
using Framework.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using TodoApplication.Domain.TodoTasks;
using TodoApplication.Domain.TodoTasks.Adapters;

namespace TodoApplication.Persistence.TodoTasks.Ports.Output;

public class TodoTaskRepository(DbContext context) : RepositoryBase<TodoTask, long>(context), ITodoTaskRepository
{
    protected override IList<Expression<Func<TodoTask, object>>> GetIncludes()
    {
        return new List<Expression<Func<TodoTask, object>>>
        {
            task => task.TaskHistories
        };
    }
}