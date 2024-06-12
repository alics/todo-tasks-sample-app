using System.Net;
using Framework.Core.Queries;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TodoApplication.Api.DataTransferObjects;
using TodoApplication.ApplicationService.Contracts.TodoTask;
using TodoApplication.ApplicationService.Ports.Input;
using TodoApplication.Common;
using TodoApplication.ReadModel.Contracts;

namespace TodoApplication.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController(ITodoTaskApplicationService todoTaskApplicationService) : ControllerBase
{
    [HttpPost]
    [SwaggerResponse((int)HttpStatusCode.OK, "Success", typeof(CreateTaskResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateTodoTask([FromBody] CreateTaskDto createTaskDto)
    {
        var response = await todoTaskApplicationService.CreateTaskAsync(createTaskDto.Title, createTaskDto.Deadline);
        return Ok(response);
    }

    [HttpPut("{id:long}")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Success")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "TodoTask not found")]
    public async Task<IActionResult> UpdateTodoTask([FromRoute] long id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        await todoTaskApplicationService.UpdateTaskAsync(id, updateTaskDto.Title, updateTaskDto.Deadline,
            updateTaskDto.Status);
        return Ok();
    }

    [HttpGet("{id:long}")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Success", typeof(TodoTaskQueryResult))]
    public async Task<IActionResult> GetTodoTask([FromRoute] long id)
    {
        var filter = new TodoTasksQueryFilter
        {
            Id = id
        };

        var tasks = await todoTaskApplicationService.GetTasks(filter);
        var result = tasks.Items.FirstOrDefault();
        return Ok(result!);
    }

    [HttpDelete("{id:long}")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Success")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "TodoTask not found")]
    public async Task<IActionResult> DeleteTodoTask([FromRoute] long id)
    {
        await todoTaskApplicationService.DeleteTaskAsync(id);
        return Ok();
    }

    [HttpGet]
    [SwaggerResponse((int)HttpStatusCode.OK, "Success", typeof(CollectionQueryResult<TodoTaskQueryResult>))]
    public async Task<IActionResult> GetTodoTasks([FromQuery] string? title, [FromQuery] byte? status)
    {
        var filter = new TodoTasksQueryFilter();
        if (status.HasValue)
        {
            var taskStatus = (TodoTaskStatus)status;
            filter.Status = taskStatus;
        }

        if (string.IsNullOrEmpty(title) == false) filter.Title = title;

        var tasks = await todoTaskApplicationService.GetTasks(filter);
        return Ok(tasks);
    }
}