using Framework.Core;

namespace TodoApplication.ApplicationService.Contracts.TodoTask
{
    public record CreateTodoTaskCommand(long Id,string Title, DateTime Deadline) : ICommand;
}
