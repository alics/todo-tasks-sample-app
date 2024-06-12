using System.Net;
using System.Text;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TodoApplication.Api.DataTransferObjects;
using TodoApplication.Domain.TodoTasks.Adapters;
using Newtonsoft.Json;
using TodoApplication.ApplicationService.Contracts.TodoTask;
using TodoApplication.Common;
using TodoApplication.ApplicationService.Ports.Input;
using TodoApplication.ReadModel.Contracts;

namespace TodoApplication.Api.IntegrationTests;

[TestFixture]
public class TaskControllerIntegrationTests
{
    private HttpClient _client;
    private WebApplicationFactory<Startup> _factory;
    private IServiceScope _scope;

    public bool CleanupRepoAfterTest => true;

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Startup>();
        _client = _factory.CreateClient();
    }

    [Test]
    public async Task CreateTodoTask_ValidData_ShouldReturnOk()
    {
        //arrange
        var title = "My Test Task";
        var deadline = DateTime.Today.AddDays(2);
        var createTaskDto = new CreateTaskDto(title, deadline);
        var content = GetContent(createTaskDto);

        //act
        var response = await _client.PostAsync("/api/tasks", content);

        //assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var createTaskResponse = await GetDeserializedResponseAsync<CreateTaskResponse>(response);

        var todoTaskRepository = GetServiceProvider().GetRequiredService<ITodoTaskRepository>();
        var todoTask = await todoTaskRepository.GetAsync(createTaskResponse.TaskId);
        Assert.AreEqual(title, todoTask.Title);
        Assert.AreEqual(deadline.Date, todoTask.Deadline.Date);
        Assert.AreEqual(TodoTaskStatus.Created, todoTask.Status);

        Assert.IsNotNull(todoTask.TaskHistories);
        Assert.AreEqual(1, todoTask.TaskHistories.Count());
        Assert.AreEqual(TodoTaskStatus.Created, todoTask.TaskHistories.Single().TaskStatus);

        //cleanup
        if (CleanupRepoAfterTest) 
            await CleanupRepo(createTaskResponse.TaskId);
    }

    [TestCase("ShortTitle", 1)]
    [TestCase("Valid Title", -1)]
    [TestCase("Valid Title", 0)]
    [TestCase(null, 1)]
    public async Task CreateTodoTask_InvalidData_ShouldReturnBadRequest(string? title, int daysDifference)
    {
        //arrange
        var deadline = DateTime.Today.AddDays(daysDifference);
        var createTaskDto = new CreateTaskDto(title, deadline);
        var requestContent = GetContent(createTaskDto);

        //act
        var response = await _client.PostAsync("/api/tasks", requestContent);

        //assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task UpdateTask_ValidData_ShouldReturnOk()
    {
        //arrange
        var todoTaskApplicationService = GetServiceProvider().GetRequiredService<ITodoTaskApplicationService>();
        var createTaskResponse = await todoTaskApplicationService.CreateTaskAsync("my old title", DateTime.Today.AddDays(1));

        var updatedTitle = "my updated title";
        var updatedDeadline = DateTime.Today.AddDays(2);
        var updatedStatus = TodoTaskStatus.Done;
        var updateTaskDto = new UpdateTaskDto(updatedTitle, updatedDeadline, updatedStatus);
        var requestContent = GetContent(updateTaskDto);

        //act
        var response = await _client.PutAsync($"/api/tasks/{createTaskResponse.TaskId}", requestContent);

        //arrange
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var todoTaskRepository = GetServiceProvider().GetRequiredService<ITodoTaskRepository>();
        var updatedTodoTask = await todoTaskRepository.GetAsync(createTaskResponse.TaskId);
        Assert.AreEqual(updatedTitle, updatedTodoTask.Title);
        Assert.AreEqual(updatedDeadline, updatedTodoTask.Deadline.Date);
        Assert.AreEqual(updatedStatus, updatedTodoTask.Status);

        Assert.IsNotNull(updatedTodoTask.TaskHistories);
        Assert.AreEqual(2, updatedTodoTask.TaskHistories.Count());
        Assert.AreEqual(updatedStatus, updatedTodoTask.TaskHistories.Last().TaskStatus);
        Assert.AreEqual(TodoTaskStatus.Created, updatedTodoTask.TaskHistories.First().TaskStatus);

        //cleanup
        if (CleanupRepoAfterTest)
            await CleanupRepo(createTaskResponse.TaskId);
    }

    [TestCase("ShortTitle", 1)]
    [TestCase("Valid Title", -1)]
    [TestCase("Valid Title", 0)]
    [TestCase(null, 1)]
    public async Task UpdateTask_InvalidData_ShouldReturnBadRequest(string? updatedTitle, int daysDifference)
    {
        //arrange
        var todoTaskApplicationService = GetServiceProvider().GetRequiredService<ITodoTaskApplicationService>();
        var createTaskResponse = await todoTaskApplicationService.CreateTaskAsync("my old title", DateTime.Today.AddDays(1));

        var updatedDeadline = DateTime.Today.AddDays(daysDifference);
        var updatedStatus = TodoTaskStatus.Done;
        var updateTaskDto = new UpdateTaskDto(updatedTitle, updatedDeadline, updatedStatus);
        var requestContent = GetContent(updateTaskDto);

        //act
        var response = await _client.PutAsync($"/api/tasks/{createTaskResponse.TaskId}", requestContent);

        //arrange
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        //cleanup
        if (CleanupRepoAfterTest)
            await CleanupRepo(createTaskResponse.TaskId);
    }

    [Test]
    public async Task UpdateTask_NotExistingId_ShouldReturnBadRequest()
    {
        //arrange
        var nonExistingTaskId = 1;
        var updatedTitle = "my updated title";
        var updatedDeadline = DateTime.Today.AddDays(2);
        var updatedStatus = TodoTaskStatus.Done;
        var updateTaskDto = new UpdateTaskDto(updatedTitle, updatedDeadline, updatedStatus);
        var requestContent = GetContent(updateTaskDto);

        //act
        var response = await _client.PutAsync($"/api/tasks/{nonExistingTaskId}", requestContent);

        //arrange
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Test]
    public async Task GetTask_ExistingTaskId_ShouldReturnTask()
    {
        //arrange
        var todoTaskApplicationService = GetServiceProvider().GetRequiredService<ITodoTaskApplicationService>();
        var title = "my task title";
        var deadline= DateTime.Today.AddDays(10);
        var createTaskResponse = await todoTaskApplicationService.CreateTaskAsync(title, deadline);

        //act
        var response = await _client.GetAsync($"/api/tasks/{createTaskResponse.TaskId}");
        
        //assert
        Assert.AreEqual(HttpStatusCode.OK,response.StatusCode);

        var todoTask = await GetDeserializedResponseAsync<TodoTaskQueryResult?>(response);
        Assert.AreEqual(title, todoTask.Title);
        Assert.AreEqual(deadline, todoTask.Deadline);
        Assert.AreEqual(TodoTaskStatus.Created, todoTask.Status);

        //cleanup
        if (CleanupRepoAfterTest)
            await CleanupRepo(createTaskResponse.TaskId);
    }

    [Test]
    public async Task GetTask_NonExistingTaskId_ShouldReturnNoContent()
    {
        //arrange
        var nonExistingTaskId = 1;

        //act
        var response = await _client.GetAsync($"/api/tasks/{nonExistingTaskId}");

        //assert
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Test]
    public async Task DeleteTask_ExistingTaskId_ShouldReturnOk()
    {
        //arrange
        var todoTaskApplicationService = GetServiceProvider().GetRequiredService<ITodoTaskApplicationService>();
        var createTaskResponse = await todoTaskApplicationService.CreateTaskAsync("my task title", DateTime.Today.AddDays(10));
        
        //act
        var response = await _client.DeleteAsync($"/api/tasks/{createTaskResponse.TaskId}");

        //assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var todoTaskRepository = GetServiceProvider().GetRequiredService<ITodoTaskRepository>();
        var todoTask = await todoTaskRepository.GetAsync(createTaskResponse.TaskId);
        Assert.IsNull(todoTask);
    }

    [Test]
    public async Task DeleteTask_NonExistingTaskId_ShouldReturnNotFound()
    {
        //arrange
        var nonExistingTaskId = 1;

        //act
        var response = await _client.DeleteAsync($"/api/tasks/{nonExistingTaskId}");

        //assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Test]
    public async Task GetTasks_ShouldReturnTasks()
    {
        //arrange
        var todoTaskApplicationService = GetServiceProvider().GetRequiredService<ITodoTaskApplicationService>();
        var title = "my task title";
        var deadline = DateTime.Today.AddDays(10);
        
        var todoTask1 = await todoTaskApplicationService.CreateTaskAsync(title, deadline);
        var todoTask2 = await todoTaskApplicationService.CreateTaskAsync(title, deadline);
        var todoTask3 = await todoTaskApplicationService.CreateTaskAsync("different title", deadline);
        var todoTask4 = await todoTaskApplicationService.CreateTaskAsync(title, deadline.AddDays(1));
        await todoTaskApplicationService.UpdateTaskAsync(todoTask4.TaskId, title, deadline.AddDays(2), TodoTaskStatus.Started);

        //act
        var response = await _client.GetAsync($"/api/tasks?title={title}&status={(byte)TodoTaskStatus.Created}");

        //assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        
        var todoTasks = await GetDeserializedResponseAsync<MyCollectionQueryResult<TodoTaskQueryResult>>(response);
        Assert.IsNotNull(todoTasks);
        Assert.GreaterOrEqual(todoTasks.TotalItems, 2);
        foreach (var todoTask in todoTasks.Items)
        {
            Assert.AreEqual(title,todoTask.Title);
            Assert.AreEqual(TodoTaskStatus.Created, todoTask.Status);
        }

        //cleanup
        if (CleanupRepoAfterTest)
        {
            await CleanupRepo(todoTask1.TaskId);
            await CleanupRepo(todoTask2.TaskId);
            await CleanupRepo(todoTask3.TaskId);
            await CleanupRepo(todoTask4.TaskId);
        }
    }

    [TearDown]
    public async Task TearDown()
    {
        _client.Dispose();
        _factory.Dispose();
        _scope?.Dispose(); 
    }

    private IServiceProvider GetServiceProvider()
    {
        _scope = _factory.Services.CreateScope();
        return _scope.ServiceProvider;
    }

    private async Task<TResponse> GetDeserializedResponseAsync<TResponse>(HttpResponseMessage response)
    {
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter>
            {
                new LongConverter(),
                new NullableLongConverter()
            }
        };
        return JsonConvert.DeserializeObject<TResponse>(jsonResponse, settings);
    }

    private StringContent GetContent(object data)
    {
        if (data != null)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            return content;
        }
        return null;
    }

    private async Task CleanupRepo(long taskId)
    {
        var todoTaskApplicationService = GetServiceProvider().GetRequiredService<ITodoTaskApplicationService>();
        await todoTaskApplicationService.DeleteTaskAsync(taskId);
    }

    private class MyCollectionQueryResult<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}