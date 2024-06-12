using Framework.Domain;
using TodoApplication.Common;

namespace TodoApplication.Domain.TodoTasks;

/// <summary>
/// Represents a historical record of a task's status change.
/// </summary>
public class TaskHistory : ValueObject<TaskHistory>
{
    /// <summary>
    /// Gets the ID of the task associated with this history record.
    /// </summary>
    public long TaskId { get; private set; }

    /// <summary>
    /// Gets the status of the task at the time of this history record.
    /// </summary>
    public TodoTaskStatus TaskStatus { get; private set; }

    /// <summary>
    /// Gets the date and time when this history record was created.
    /// </summary>
    public DateTime DateTime { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskHistory"/> class.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <param name="taskStatus">The status of the task.</param>
    public TaskHistory(long taskId, TodoTaskStatus taskStatus)
    {
        TaskId = taskId;
        TaskStatus = taskStatus;
        DateTime = DateTime.Now;
    }

    /// <summary>
    /// Retrieves the equality components used for comparison.
    /// </summary>
    /// <returns>An enumerable collection of equality components.</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return TaskId;
        yield return TaskStatus;
    }
}