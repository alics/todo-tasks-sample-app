namespace TodoApplication.Domain.TodoTasks.Adapters;

public interface ITodoTaskRepository
{
    Task InsertAsync(TodoTask task);
    Task<TodoTask?> GetAsync(long id);
    void Delete(TodoTask task);
}