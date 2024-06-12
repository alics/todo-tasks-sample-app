using TodoApplication.Common;

namespace TodoApplication.ApplicationService.Contracts.TodoTask;

public record UpdateTodoTaskCommand(long Id, string Title, DateTime Deadline, TodoTaskStatus Status)
    : CreateTodoTaskCommand(Id, Title, Deadline);