using TodoApplication.Common;
using TodoApplication.Domain.TodoTasks;

namespace TodoApplication.Domain.UnitTests;

public class TaskHistoryTest
{
    [TestCase(TodoTaskStatus.Created)]
    [TestCase(TodoTaskStatus.Started)]
    [TestCase(TodoTaskStatus.Done)]
    public void CreateTaskHistory_ShouldReturnTaskHistory(TodoTaskStatus todoTaskStatus)
    {
        var taskId = 1;
        var taskHistory = new TaskHistory(taskId, todoTaskStatus);
        
        Assert.AreEqual(taskId, taskHistory.TaskId);
        Assert.AreEqual(todoTaskStatus, taskHistory.TaskStatus);
    }
}