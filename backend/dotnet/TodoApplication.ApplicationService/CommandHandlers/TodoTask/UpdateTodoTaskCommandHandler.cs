using Framework.Application;
using Framework.Core;
using TodoApplication.ApplicationService.Contracts.TodoTask;
using TodoApplication.ApplicationService.Exceptions;
using TodoApplication.Domain.TodoTasks.Adapters;

namespace TodoApplication.ApplicationService.CommandHandlers.TodoTask;

/// <summary>
/// Handles the command to update a todo task.
/// </summary>
public class UpdateTodoTaskCommandHandler : CommandHandlerBase<UpdateTodoTaskCommand>
{
    private readonly ITodoTaskRepository _todoTaskRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTodoTaskCommandHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work for managing transactions and database operations.</param>
    /// <param name="todoTaskRepository">The repository for todo tasks.</param>
    public UpdateTodoTaskCommandHandler(IUnitOfWork unitOfWork, ITodoTaskRepository todoTaskRepository)
        : base(unitOfWork)
    {
        this._todoTaskRepository = todoTaskRepository;
    }

    /// <summary>
    /// Handles the asynchronous execution of the update todo task command.
    /// </summary>
    /// <param name="command">The command to update a todo task.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public override async Task HandleAsync(UpdateTodoTaskCommand command)
    {
        var currentTask = await _todoTaskRepository.GetAsync(command.Id);

        // If the task does not exist, throw a TodoTaskNotFoundException
        if (currentTask is null)
        {
            throw new TodoTaskNotFoundException(command.Id);
        }

        currentTask.UpdateDeadline(command.Deadline);
        currentTask.UpdateTitle(command.Title);
        currentTask.UpdateStatus(command.Status);
    }
}