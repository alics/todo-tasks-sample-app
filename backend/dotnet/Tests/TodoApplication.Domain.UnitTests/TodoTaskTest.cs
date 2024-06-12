using TodoApplication.Common;
using TodoApplication.Domain.TodoTasks;
using TodoApplication.Domain.TodoTasks.Exceptions;

namespace TodoApplication.Domain.UnitTests;

public class Tests
{
    [Test]
    public void CreateTask_ValidData_ShouldReturnTask()
    {
        //arrange
        var id = 1;
        var title = "Task1234567";
        var deadline = DateTime.Today.AddDays(1);

        //act
        var todoTask = TodoTask.CreateTask(id, title, deadline);

        //assert
        Assert.AreEqual(id, todoTask.Id);
        Assert.AreEqual(title, todoTask.Title);
        Assert.AreEqual(deadline, todoTask.Deadline);
        Assert.AreEqual(TodoTaskStatus.Created, todoTask.Status);
        
        Assert.IsNotNull(todoTask.TaskHistories);
        Assert.AreEqual(1, todoTask.TaskHistories.Count());
        Assert.AreEqual(TodoTaskStatus.Created, todoTask.TaskHistories.Single().TaskStatus);
    }

    [TestCase(-1)]
    [TestCase(0)]
    public void CreateTask_DeadlineIsLessThanOrEqualToday_ShouldThrowInvalidTaskDeadlineException(int daysDifference)
    {
        //arrange
        var id = 1;
        var title = "Task1234567";
        var deadline = DateTime.Today.AddDays(daysDifference);

        //act & assert
        Assert.Throws<InvalidTaskDeadlineException>(() => TodoTask.CreateTask(id, title, deadline));
    }

    [TestCase("Task123456")]
    [TestCase("Task")]
    [TestCase("")]
    [TestCase(null)]
    public void CreateTask_TitleIsLessThanOrEqual10Characters_ShouldThrowInvalidTaskTitleException(string title)
    {
        //arrange
        var id = 1;
        var deadline = DateTime.Today.AddDays(1);

        //act & assert
        Assert.Throws<InvalidTaskTitleException>(() => TodoTask.CreateTask(id, title, deadline));
    }

    [TestCase("Task1234567")]
    [TestCase("Task12345678")]
    public void UpdateTitle_TitleIsLongerThan10Characters_ShouldUpdateTitle(string newTitle)
    {
        //arrange
        var todoTask = TodoTask.CreateTask(1, "My task title", DateTime.Today.AddDays(2));

        //act
        todoTask.UpdateTitle(newTitle);

        //assert
        Assert.AreEqual(newTitle, todoTask.Title);
    }

    [TestCase("MyTask1234")]
    [TestCase("Task")]
    [TestCase("")]
    [TestCase(null)]
    public void UpdateTitle_TitleIsLessThanOrEqual10Characters_ShouldThrowInvalidTaskTitleException(string newTitle)
    {
        //arrange
        var todoTask = TodoTask.CreateTask(1, "My task title", DateTime.Today.AddDays(2));

        //act & assert
        Assert.Throws<InvalidTaskTitleException>(() => todoTask.UpdateTitle(newTitle));
    }

    [Test]
    public void UpdateDeadline_DeadlineIsGreaterThanToday_ShouldUpdateDeadline()
    {
        //arrange
        var todoTask = TodoTask.CreateTask(1, "My task title", DateTime.Today.AddDays(2));
        var newDeadline = DateTime.Today.AddDays(3);

        //act
        todoTask.UpdateDeadline(newDeadline);

        //assert
        Assert.AreEqual(newDeadline, todoTask.Deadline);
    }

    [TestCase(-1)]
    [TestCase(0)]
    public void UpdateDeadline_DeadlineIsLessThanOrEqualToday_ShouldThrowInvalidTaskDeadlineException(int daysDifference)
    {
        //arrange
        var todoTask = TodoTask.CreateTask(1, "My task title", DateTime.Today.AddDays(2));
        var newDeadline = DateTime.Today.AddDays(daysDifference);

        //act & assert
        Assert.Throws<InvalidTaskDeadlineException>(() => todoTask.UpdateDeadline(newDeadline));
    }

    [TestCase(TodoTaskStatus.Done)]
    [TestCase(TodoTaskStatus.Started)]
    public void UpdateTaskStatus_ShouldUpdateTaskStatusAndAddToTaskHistory(TodoTaskStatus newStatus)
    {
        //arrange
        var id = 1;
        var todoTask = TodoTask.CreateTask(id, "My task title", DateTime.Today.AddDays(2));

        //act
        todoTask.UpdateStatus(newStatus);

        //assert
        Assert.AreEqual(newStatus, todoTask.Status);
        
        Assert.IsNotNull(todoTask.TaskHistories);
        Assert.AreEqual(2, todoTask.TaskHistories.Count());
        Assert.AreEqual(newStatus, todoTask.TaskHistories.Last().TaskStatus);
        Assert.AreEqual(TodoTaskStatus.Created, todoTask.TaskHistories.First().TaskStatus);
    }

    [Test]
    public void UpdateTaskStatus_NewStatusEqualToCurrentStatus_ShouldNotAddToTaskHistory()
    {
        //arrange
        var id = 1;
        var todoTask = TodoTask.CreateTask(id, "My task title", DateTime.Today.AddDays(2));

        //act
        todoTask.UpdateStatus(TodoTaskStatus.Created);

        //assert
        Assert.AreEqual(TodoTaskStatus.Created, todoTask.Status);

        Assert.IsNotNull(todoTask.TaskHistories);
        Assert.AreEqual(1, todoTask.TaskHistories.Count());
        Assert.NotNull(todoTask.TaskHistories.FirstOrDefault(x => x.TaskId == id && x.TaskStatus == TodoTaskStatus.Created));
    }
}