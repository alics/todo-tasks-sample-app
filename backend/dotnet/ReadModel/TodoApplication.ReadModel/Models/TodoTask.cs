using TodoApplication.Common;

namespace TodoApplication.ReadModel.Models;

public class TodoTask
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime Deadline { get; set; }

    public TodoTaskStatus Status { get; set; }

    public virtual ICollection<TaskHistory> TaskHistories { get; set; } = new List<TaskHistory>();
}