using Framework.Core.Queries;
using TodoApplication.ApplicationService.Contracts.TodoTask;
using TodoApplication.Common;
using TodoApplication.ReadModel.Contracts;

namespace TodoApplication.ApplicationService.Ports.Input
{
    /// <summary>
    /// Interface for managing tasks in the application layer.
    /// </summary>
    public interface ITodoTaskApplicationService
    {
        /// <summary>
        /// Creates a new task asynchronously.
        /// </summary>
        /// <param name="title">The title of the task.</param>
        /// <param name="deadlineDate">The deadline date of the task.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response of the creation operation.</returns>
        Task<CreateTaskResponse> CreateTaskAsync(string title, DateTime deadlineDate);

        /// <summary>
        /// Updates an existing task asynchronously.
        /// </summary>
        /// <param name="id">The ID of the task to update.</param>
        /// <param name="title">The new title of the task.</param>
        /// <param name="deadlineDate">The new deadline date of the task.</param>
        /// <param name="status">The new status of the task.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateTaskAsync(long id, string title, DateTime deadlineDate, TodoTaskStatus status);

        /// <summary>
        /// Retrieves tasks based on the specified filter asynchronously.
        /// </summary>
        /// <param name="filter">The filter criteria for retrieving tasks.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the collection query result of todo tasks.</returns>
        Task<CollectionQueryResult<TodoTaskQueryResult>> GetTasks(TodoTasksQueryFilter filter);

        /// <summary>
        /// Deletes a task asynchronously.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteTaskAsync(long id);
    }
}
