using Framework.Application;
using Framework.Core;
using TodoApplication.ApplicationService.Contracts.TodoTask;
using TodoApplication.Domain.TodoTasks.Adapters;

namespace TodoApplication.ApplicationService.CommandHandlers.TodoTask;

/// <summary>
///     Represents a command handler for creating a new task.
/// </summary>
public class CreateTodoTaskCommandHandler : CommandHandlerBase<CreateTodoTaskCommand>
{
    private readonly ITodoTaskRepository _todoTaskRepository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateTodoTaskCommandHandler" /> class
    ///     with the specified unit of work and task repository.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used to manage the transaction.</param>
    /// <param name="todoTaskRepository">The repository for accessing task data.</param>
    public CreateTodoTaskCommandHandler(IUnitOfWork unitOfWork, ITodoTaskRepository todoTaskRepository)
        : base(unitOfWork)
    {
        _todoTaskRepository = todoTaskRepository;
    }

    /// <summary>
    ///     Handles the creation of a new task asynchronously.
    /// </summary>
    /// <param name="command">The command containing information about the task to be created.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public override async Task HandleAsync(CreateTodoTaskCommand command)
    {
        var task = Domain.TodoTasks.TodoTask.CreateTask(command.Id, command.Title, command.Deadline);
        await _todoTaskRepository.InsertAsync(task);
    }
}