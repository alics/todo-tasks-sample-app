using Framework.Core;
using Moq;
using NUnit.Framework;
using TodoApplication.ApplicationService.CommandHandlers.TodoTask;
using TodoApplication.ApplicationService.Contracts.TodoTask;
using TodoApplication.ApplicationService.Exceptions;
using TodoApplication.Common;
using TodoApplication.Domain.TodoTasks;
using TodoApplication.Domain.TodoTasks.Adapters;

namespace TodoApplication.ApplicationService.UnitTests;

[TestFixture]
public class UpdateTodoTaskCommandHandlerTests
{
    [Test]
    public async Task HandleUpdateTodoTaskCommand_TaskExists_ShouldUpdateTask()
    {
        //arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
        var todoTaskRepositoryMock = new Mock<ITodoTaskRepository>(MockBehavior.Strict);

        var taskId = 1;
        var todoTask = TodoTask.CreateTask(taskId, "Existing TodoTask", DateTime.Now.AddDays(1));

        todoTaskRepositoryMock.Setup(repo => repo.GetAsync(taskId))
            .ReturnsAsync(todoTask);

        var handler = new UpdateTodoTaskCommandHandler(unitOfWorkMock.Object, todoTaskRepositoryMock.Object);
        var command = new UpdateTodoTaskCommand(taskId, "Updated TodoTask", DateTime.Now.AddDays(3), TodoTaskStatus.Done);

        //act
        await handler.HandleAsync(command);

        //assert
        todoTaskRepositoryMock.Verify(repo => repo.GetAsync(taskId), Times.Once);

        Assert.AreEqual(command.Title, todoTask.Title);
        Assert.AreEqual(command.Deadline.Date, todoTask.Deadline.Date);
        Assert.AreEqual(command.Status, todoTask.Status);

        Assert.IsNotNull(todoTask.TaskHistories);
        Assert.AreEqual(2, todoTask.TaskHistories.Count());
        Assert.AreEqual(command.Status, todoTask.TaskHistories.Last().TaskStatus);
        Assert.AreEqual(TodoTaskStatus.Created, todoTask.TaskHistories.First().TaskStatus);
    }

    [Test]
    public void HandleUpdateTodoTaskCommand_NonExistingTask_ShouldThrowException()
    {
        //arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var todoTaskRepositoryMock = new Mock<ITodoTaskRepository>();
        var nonExistingTaskId = 1;

        var handler = new UpdateTodoTaskCommandHandler(unitOfWorkMock.Object, todoTaskRepositoryMock.Object);
        var command = new UpdateTodoTaskCommand(nonExistingTaskId, "Updated Task", DateTime.Now.AddDays(1), TodoTaskStatus.Started);

        //act & assert
        Assert.ThrowsAsync<TodoTaskNotFoundException>(async () => await handler.HandleAsync(command));
    }
}