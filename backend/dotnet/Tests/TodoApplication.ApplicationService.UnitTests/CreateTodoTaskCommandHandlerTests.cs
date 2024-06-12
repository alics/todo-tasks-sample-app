using Framework.Core;
using Moq;
using NUnit.Framework;
using TodoApplication.ApplicationService.CommandHandlers.TodoTask;
using TodoApplication.ApplicationService.Contracts.TodoTask;
using TodoApplication.Domain.TodoTasks;
using TodoApplication.Domain.TodoTasks.Adapters;

namespace TodoApplication.ApplicationService.UnitTests;

[TestFixture]
public class CreateTodoTaskCommandHandlerTests
{
    [Test]
    public async Task HandleCreateTodoTaskCommand_ValidCommand_ShouldInsertTask()
    {
        //arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
        var todoTaskRepositoryMock = new Mock<ITodoTaskRepository>();

        var handler = new CreateTodoTaskCommandHandler(unitOfWorkMock.Object, todoTaskRepositoryMock.Object);
        var command = new CreateTodoTaskCommand(1, "Test1234567", DateTime.Now.AddDays(1));

        //act
        await handler.HandleAsync(command);

        //assert
        todoTaskRepositoryMock.Verify(repo => repo.InsertAsync(It.IsAny<TodoTask>()), Times.Once);
    }
}