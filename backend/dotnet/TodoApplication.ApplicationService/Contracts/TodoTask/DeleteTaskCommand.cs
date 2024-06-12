using Framework.Core;

namespace TodoApplication.ApplicationService.Contracts.TodoTask;

public record DeleteTodoTaskCommand(long Id) : ICommand;