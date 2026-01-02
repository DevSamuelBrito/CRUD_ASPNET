namespace CRUD_ASPNET.Domain.Entities;

public class Tasks
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.ToDo;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
