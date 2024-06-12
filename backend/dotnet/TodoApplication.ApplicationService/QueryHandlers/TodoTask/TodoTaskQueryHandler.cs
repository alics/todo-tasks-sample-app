using Framework.Core.Queries;
using Microsoft.EntityFrameworkCore;
using TodoApplication.ReadModel;
using TodoApplication.ReadModel.Contracts;

namespace TodoApplication.ApplicationService.QueryHandlers.TodoTask;

/// <summary>
/// Handler for querying todo tasks.
/// </summary>
public class TodoTaskQueryHandler : IQueryHandler<TodoTasksQueryFilter, CollectionQueryResult<TodoTaskQueryResult>>
{
    private readonly TodoAppReadContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="TodoTaskQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The read context for accessing task data.</param>
    public TodoTaskQueryHandler(TodoAppReadContext context)
    {
        this.context = context;
    }

    /// <inheritdoc />
    /// <summary>
    /// Handles the asynchronous execution of the tasks query.
    /// </summary>
    /// <param name="filter">The filter criteria for querying tasks.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the collection query result of task query results.</returns>
    public async Task<CollectionQueryResult<TodoTaskQueryResult>> HandleAsync(TodoTasksQueryFilter filter)
    {
        var query = context.TodoTasks.AsQueryable();

        if (filter.Id.HasValue)
        {
            query = query.Where(x => x.Id == filter.Id);
        }
        else
        {
            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(x => x.Title.Contains(filter.Title));

            if (filter.Status.HasValue)
                query = query.Where(x => x.Status == filter.Status);
        }

        var result = await query.Select(task => new TodoTaskQueryResult
        {
            Id = task.Id,
            Title = task.Title,
            Status = task.Status,
            Deadline = task.Deadline.Date,
            CreationDate = task.CreationDate
        }).ToListAsync();

        return new CollectionQueryResult<TodoTaskQueryResult>(result);
    }
}