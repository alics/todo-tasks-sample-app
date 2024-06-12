using Framework.Core.Exceptions;
using TodoApplication.Resources;

namespace TodoApplication.ApplicationService.Exceptions;

/// <summary>
/// Exception thrown when a task is not found.
/// </summary>
public class TodoTaskNotFoundException : BaseApplicationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TodoTaskNotFoundException"/> class.
    /// </summary>
    /// <param name="id">The ID of the task that was not found.</param>
    public TodoTaskNotFoundException(long id) : base(string.Format(ExceptionMessages.InvalidTaskDeadlineException, id))
    {
    }

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public override int StatusCode => 404;
}