using Framework.Core.Queries;
using TodoApplication.Common;

namespace TodoApplication.ReadModel.Contracts;

public class TodoTaskQueryResult : IQueryResult
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime Deadline { get; set; }

    public TodoTaskStatus Status { get; set; }
}