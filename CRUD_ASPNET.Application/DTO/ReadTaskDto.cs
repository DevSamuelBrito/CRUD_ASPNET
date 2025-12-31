namespace CRUD_ASPNET.Application.DTO;

public class ReadTaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Domain.Entities.TaskStatus Status { get; set; }
}
