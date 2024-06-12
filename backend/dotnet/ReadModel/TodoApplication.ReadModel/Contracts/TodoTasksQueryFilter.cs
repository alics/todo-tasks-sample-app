using Framework.Core.Queries;
using TodoApplication.Common;

namespace TodoApplication.ReadModel.Contracts;

public class TodoTasksQueryFilter : IQueryFilter
{
    public long? Id { get; set; }
    public string? Title { get; set; }
    public TodoTaskStatus? Status { get; set; }
}