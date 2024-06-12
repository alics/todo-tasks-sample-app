using TodoApplication.Common;

namespace TodoApplication.Api.DataTransferObjects;

public class UpdateTaskDto(string title, DateTime deadlineDate, TodoTaskStatus status)
    : CreateTaskDto(title, deadlineDate)
{
    public TodoTaskStatus Status { get; } = status;
}