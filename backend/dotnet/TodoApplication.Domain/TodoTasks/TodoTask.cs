using Framework.Domain;
using TodoApplication.Common;
using TodoApplication.Domain.TodoTasks.Exceptions;

namespace TodoApplication.Domain.TodoTasks;

/// <summary>
///     Represents task entity.
/// </summary>
public class TodoTask : AggregateRoot<long>
{
    private readonly List<TaskHistory> _taskHistories;

    public TodoTask() {}

    /// <summary>
    ///     Initializes a new instance of the <see cref="TodoTask" /> class with the specified id, title, and deadline.
    /// </summary>
    /// <param name="id">The unique identifier of the task.</param>
    /// <param name="title">The title of the task.</param>
    /// <param name="deadline">The deadline of the task.</param>
    private TodoTask(long id, string title, DateTime deadline)
    {
        Title = title;
        Id = id;
        CreationDate = DateTime.Now;
        UpdateTitle(title);
        UpdateDeadline(deadline);

        Status = TodoTaskStatus.Created;
        _taskHistories = new List<TaskHistory> { new(Id, TodoTaskStatus.Created) };
    }

    /// <summary>
    ///     Gets the title of the task.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    ///     Gets the creation date of the task.
    /// </summary>
    public DateTime CreationDate { get; private set; }

    /// <summary>
    ///     Gets the deadline of the task.
    /// </summary>
    public DateTime Deadline { get; private set; }

    /// <summary>
    ///     Gets the status of the task.
    /// </summary>
    public TodoTaskStatus Status { get; private set; }

    /// <summary>
    ///     Gets the list of task histories associated with the task.
    /// </summary>
    public IEnumerable<TaskHistory> TaskHistories => _taskHistories.AsReadOnly();

    /// <summary>
    ///     Creates a new to do task with the specified id, title, and deadline.
    /// </summary>
    /// <param name="id">The unique identifier of the to do task.</param>
    /// <param name="title">The title of the t odo task.</param>
    /// <param name="deadline">The deadline of the to do task.</param>
    /// <returns>A new instance of <see cref="TodoTask" />.</returns>
    public static TodoTask CreateTask(long id, string title, DateTime deadline)
    {
        return new TodoTask(id, title, deadline);
    }

    /// <summary>
    ///     Updates the title of the task.
    /// </summary>
    /// <param name="title">The new title of the task.</param>
    /// <exception cref="InvalidTaskTitleException">Thrown when the title is null or has less than 11 characters.</exception>
    public void UpdateTitle(string title)
    {
        if (title == null || title.Length < 11) throw new InvalidTaskTitleException();
        Title = title;
    }

    /// <summary>
    ///     Updates the deadline of the task.
    /// </summary>
    /// <param name="deadline">The new deadline of the task.</param>
    /// <exception cref="InvalidTaskDeadlineException">Thrown when the deadline is before or equal to the current date.</exception>
    public void UpdateDeadline(DateTime deadline)
    {
        deadline = deadline.Date;

        ValidateTaskDeadline(deadline);
        Deadline = deadline;
    }

    /// <summary>
    ///     Updates the status of the task.
    /// </summary>
    /// <param name="status">The new status of the to do task.</param>
    public void UpdateStatus(TodoTaskStatus status)
    {
        if (Status != status)
        {
            Status = status;
            _taskHistories.Add(new TaskHistory(Id, status));
        }
    }

    private static void ValidateTaskDeadline(DateTime deadline)
    {
        if (deadline <= DateTime.Today)
            throw new InvalidTaskDeadlineException();
    }
}