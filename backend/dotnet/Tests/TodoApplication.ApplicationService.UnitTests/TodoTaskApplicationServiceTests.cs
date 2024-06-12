using Framework.Application;
using Framework.Core.Queries;
using Framework.Core;
using Moq;
using NUnit.Framework;
using TodoApplication.ApplicationService.Contracts.TodoTask;
using TodoApplication.Common;
using TodoApplication.ReadModel.Contracts;

namespace TodoApplication.ApplicationService.UnitTests;

[TestFixture]
public class TodoTaskApplicationServiceTests
{
    [Test]
    public async Task CreateTaskAsync_ShouldReturnCreateTaskResponse()
    {
        //arrange
        var idGeneratorMock = new Mock<IIdGenerator>();
        var commandBusMock = new Mock<ICommandBus>();
        var service = new TodoTaskApplicationService(commandBusMock.Object, null, idGeneratorMock.Object);

        var taskId = 1;
        var title = "My Test Task";
        var deadlineDate = DateTime.Now.AddDays(1);

        idGeneratorMock.Setup(generator => generator.Create())
                       .Returns(taskId);

        //act
        var response = await service.CreateTaskAsync(title, deadlineDate);

        //assert
        Assert.AreEqual(taskId, response.TaskId);
        Assert.AreEqual($"{title} task created successfully", response.Message);
        commandBusMock.Verify(bus => bus.DispatchAsync(It.IsAny<CreateTodoTaskCommand>()), Times.Once);
    }

    [Test]
    public async Task UpdateTaskAsync_ShouldDispatchUpdateTodoTaskCommand()
    {
        //arrange
        var commandBusMock = new Mock<ICommandBus>();
        var service = new TodoTaskApplicationService(commandBusMock.Object, null, null);

        var taskId = 1;
        var title = "My Test Task";
        var deadlineDate = DateTime.Today.AddDays(1);
        var status = TodoTaskStatus.Done;

        //act
        await service.UpdateTaskAsync(taskId, title, deadlineDate, status);

        //assert
        commandBusMock.Verify(bus => bus.DispatchAsync(It.IsAny<UpdateTodoTaskCommand>()), Times.Once);
    }

    [Test]
    public async Task GetTasks_ShouldReturnTasks()
    {
        //arrange
        var queryBusMock = new Mock<IQueryBus>(MockBehavior.Strict);
        var service = new TodoTaskApplicationService(null, queryBusMock.Object, null);

        var filter = new TodoTasksQueryFilter();
        var expectedResult = new CollectionQueryResult<TodoTaskQueryResult>(new[]
        {
            new TodoTaskQueryResult()
        });

        queryBusMock.Setup(bus => bus.Dispatch<TodoTasksQueryFilter, CollectionQueryResult<TodoTaskQueryResult>>(filter))
                    .ReturnsAsync(expectedResult);

        //act
        var result = await service.GetTasks(filter);

        //assert
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public async Task DeleteTaskAsync_ShouldDispatchDeleteTodoTaskCommand()
    {
        //arrange
        var commandBusMock = new Mock<ICommandBus>();
        var service = new TodoTaskApplicationService(commandBusMock.Object, null, null);

        var taskId = 1;

        //act
        await service.DeleteTaskAsync(taskId);

        //assert
        commandBusMock.Verify(bus => bus.DispatchAsync(It.IsAny<DeleteTodoTaskCommand>()), Times.Once);
    }
}
