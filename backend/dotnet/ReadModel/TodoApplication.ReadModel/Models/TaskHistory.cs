namespace TodoApplication.ReadModel.Models;

public class TaskHistory
{
    public int Id { get; set; }

    public long TaskId { get; set; }

    public byte TaskStatus { get; set; }

    public DateTime DateTime { get; set; }

    public virtual TodoTask Task { get; set; } = null!;
}