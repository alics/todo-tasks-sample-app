using Framework.Application;
using Framework.Core;
using Framework.Core.Queries;
using TodoApplication.ApplicationService.Contracts.TodoTask;
using TodoApplication.ApplicationService.Ports.Input;
using TodoApplication.Common;
using TodoApplication.ReadModel.Contracts;

namespace TodoApplication.ApplicationService
{
    /// <summary>
    /// Application service for managing tasks.
    /// </summary>
    public class TodoTaskApplicationService : ITodoTaskApplicationService
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        private readonly IIdGenerator _idGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoTaskApplicationService"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus for dispatching commands.</param>
        /// <param name="queryBus">The query bus for dispatching queries.</param>
        /// <param name="idGenerator">The ID generator for creating unique task IDs.</param>
        public TodoTaskApplicationService(ICommandBus commandBus, IQueryBus queryBus, IIdGenerator idGenerator)
        {
            this._commandBus = commandBus;
            this._queryBus = queryBus;
            this._idGenerator = idGenerator;
        }

        /// <summary>
        /// Creates a new task asynchronously.
        /// </summary>
        /// <param name="title">The title of the task.</param>
        /// <param name="deadlineDate">The deadline date of the task.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response of the creation operation.</returns>
        public async Task<CreateTaskResponse> CreateTaskAsync(string title, DateTime deadlineDate)
        {
            var id = _idGenerator.Create();
            var command = new CreateTodoTaskCommand(id, title, deadlineDate);
            await _commandBus.DispatchAsync(command);

            return new CreateTaskResponse(id, $"{title} task created successfully");
        }

        /// <summary>
        /// Updates an existing task asynchronously.
        /// </summary>
        /// <param name="id">The ID of the task to update.</param>
        /// <param name="title">The new title of the task.</param>
        /// <param name="deadlineDate">The new deadline date of the task.</param>
        /// <param name="status">The new status of the task.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateTaskAsync(long id, string title, DateTime deadlineDate, TodoTaskStatus status)
        {
            var command = new UpdateTodoTaskCommand(id, title, deadlineDate, status);
            await _commandBus.DispatchAsync(command);
        }

        /// <summary>
        /// Retrieves tasks based on the specified filter asynchronously.
        /// </summary>
        /// <param name="filter">The filter criteria for retrieving tasks.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the collection query result of todo tasks.</returns>
        public async Task<CollectionQueryResult<TodoTaskQueryResult>> GetTasks(TodoTasksQueryFilter filter)
        {
            return await _queryBus.Dispatch<TodoTasksQueryFilter, CollectionQueryResult<TodoTaskQueryResult>>(filter);
        }

        /// <summary>
        /// Deletes a task asynchronously.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteTaskAsync(long id)
        {
            var command = new DeleteTodoTaskCommand(id);
            await _commandBus.DispatchAsync(command);
        }
    }
}
