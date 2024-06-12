using Framework.Core.Exceptions;
using TodoApplication.Resources;

namespace TodoApplication.Domain.TodoTasks.Exceptions;

/// <summary>
/// Exception thrown when a task title is invalid.
/// </summary>
public class InvalidTaskTitleException : BaseApplicationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidTaskTitleException"/> class.
    /// </summary>
    public InvalidTaskTitleException() : base(ExceptionMessages.InvalidTaskTitleException)
    {
    }

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public override int StatusCode => 400;
}

