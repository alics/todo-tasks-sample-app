using Framework.Application;
using Framework.Core;
using TodoApplication.ApplicationService.Contracts.TodoTask;
using TodoApplication.ApplicationService.Exceptions;
using TodoApplication.Domain.TodoTasks.Adapters;

namespace TodoApplication.ApplicationService.CommandHandlers.TodoTask;

/// <summary>
/// Handles the command to delete a todo task.
/// </summary>
public class DeleteTodoTaskCommandHandler : CommandHandlerBase<DeleteTodoTaskCommand>
{
    private readonly ITodoTaskRepository _todoTaskRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTodoTaskCommandHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work for managing transactions and database operations.</param>
    /// <param name="todoTaskRepository">The repository for todo tasks.</param>
    public DeleteTodoTaskCommandHandler(IUnitOfWork unitOfWork, ITodoTaskRepository todoTaskRepository)
        : base(unitOfWork)
    {
        this._todoTaskRepository = todoTaskRepository;
    }

    /// <summary>
    /// Handles the asynchronous execution of the delete todo task command.
    /// </summary>
    /// <param name="command">The command to delete a todo task.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public override async Task HandleAsync(DeleteTodoTaskCommand command)
    {
        var currentTask = await _todoTaskRepository.GetAsync(command.Id);

        // If the task does not exist, throw a TodoTaskNotFoundException
        if (currentTask is null)
        {
            throw new TodoTaskNotFoundException(command.Id);
        }

        _todoTaskRepository.Delete(currentTask);
    }
}