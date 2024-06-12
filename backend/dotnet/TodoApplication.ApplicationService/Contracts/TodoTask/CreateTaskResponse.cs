namespace TodoApplication.ApplicationService.Contracts.TodoTask;

public record CreateTaskResponse(long TaskId, string Message)
{
    public long TaskId { get; set; } = TaskId;
    public string Message { get; set; } = Message;
}