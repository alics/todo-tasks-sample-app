using System.ComponentModel.DataAnnotations;

namespace TodoApplication.Api.DataTransferObjects;

public class CreateTaskDto(string title, DateTime deadlineDate)
{
    [Required] public string Title { get; } = title;

    [Required] public DateTime Deadline { get; set; } = deadlineDate;
}