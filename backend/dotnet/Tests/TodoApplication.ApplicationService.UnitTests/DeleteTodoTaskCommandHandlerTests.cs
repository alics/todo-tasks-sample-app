using Framework.Core;
using Moq;
using NUnit.Framework;
using TodoApplication.ApplicationService.CommandHandlers.TodoTask;
using TodoApplication.ApplicationService.Contracts.TodoTask;
using TodoApplication.ApplicationService.Exceptions;
using TodoApplication.Domain.TodoTasks;
using TodoApplication.Domain.TodoTasks.Adapters;

namespace TodoApplication.ApplicationService.UnitTests;

[TestFixture]
public class DeleteTodoTaskCommandHandlerTests
{
    [Test]
    public async Task HandleDeleteTodoTaskCommand_TaskExists_ShouldDeleteTask()
    {
        //arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
        var todoTaskRepositoryMock = new Mock<ITodoTaskRepository>();

        var taskId = 1;
        var todoTask = TodoTask.CreateTask(taskId, "Existing TodoTask", DateTime.Now.AddDays(1));

        todoTaskRepositoryMock.Setup(repo => repo.GetAsync(taskId))
            .ReturnsAsync(todoTask);

        var handler = new DeleteTodoTaskCommandHandler(unitOfWorkMock.Object, todoTaskRepositoryMock.Object);
        var command = new DeleteTodoTaskCommand(taskId);

        //act
        await handler.HandleAsync(command);

        //assert
        todoTaskRepositoryMock.Verify(repo => repo.Delete(todoTask), Times.Once);
    }

    [Test]
    public void HandleDeleteTodoTaskCommand_NonExistingTask_ShouldThrowException()
    {
        //arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var todoTaskRepositoryMock = new Mock<ITodoTaskRepository>();
        var nonExistingTaskId = 1;

        var handler = new DeleteTodoTaskCommandHandler(unitOfWorkMock.Object, todoTaskRepositoryMock.Object);
        var command = new DeleteTodoTaskCommand(nonExistingTaskId);

        //act & assert
        Assert.ThrowsAsync<TodoTaskNotFoundException>(async () => await handler.HandleAsync(command));
    }
}