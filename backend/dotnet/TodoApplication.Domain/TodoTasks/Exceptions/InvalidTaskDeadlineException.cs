using Framework.Core.Exceptions;
using TodoApplication.Resources;

namespace TodoApplication.Domain.TodoTasks.Exceptions;

/// <summary>
/// Exception thrown when a task deadline is invalid.
/// </summary>
public class InvalidTaskDeadlineException : BaseApplicationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidTaskDeadlineException"/> class.
    /// </summary>
    public InvalidTaskDeadlineException() : base(ExceptionMessages.InvalidTaskDeadlineException)
    {
    }

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public override int StatusCode => 400;
}