using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TodoApplication.ApplicationService.QueryHandlers.TodoTask;
using TodoApplication.Common;
using TodoApplication.ReadModel.Contracts;
using TodoApplication.ReadModel;
using TodoApplication.ReadModel.Models;

namespace TodoApplication.ApplicationService.UnitTests;

[TestFixture]
public class TodoTaskQueryHandlerTests
{
    private TodoAppReadContext _readContext;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var options = new DbContextOptionsBuilder<TodoAppReadContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _readContext = new TodoAppReadContext(options);
        var todoTasks = new List<TodoTask>
        {
            new TodoTask { Id = 1, Title = "My TodoTask 1", Status = TodoTaskStatus.Done },
            new TodoTask { Id = 2, Title = "My TodoTask 2", Status = TodoTaskStatus.Started },
            new TodoTask { Id = 3, Title = "My TodoTask 3", Status = TodoTaskStatus.Created }
        };
        _readContext.TodoTasks.AddRange(todoTasks);
        _readContext.SaveChanges();
    }


    [TestCase(1, null, null, 1)]
    [TestCase(null, "My TodoTask 2", null, 2)]
    [TestCase(null, null, TodoTaskStatus.Created, 3)]
    [TestCase(null, "My TodoTask 3", TodoTaskStatus.Created, 3)]
    public async Task HandleTodoTaskQuery_Filter_ShouldReturnFilteredTodoTasks(long? id, string? title, TodoTaskStatus? status, int expectedResultId)
    {
        //arrange
        var filter = new TodoTasksQueryFilter { Id = id, Title = title, Status = status };
        var handler = new TodoTaskQueryHandler(_readContext);

        //act
        var result = await handler.HandleAsync(filter);

        //assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Items.Count());
        var resultItem = result.Items.Single();
        Assert.AreEqual(expectedResultId, resultItem.Id);
    }
}

