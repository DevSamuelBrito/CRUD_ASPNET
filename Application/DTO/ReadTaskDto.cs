using System;

namespace CRUD_ASPNET.Application.DTO;

public class ReadTaskDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public CRUD_ASPNET.Models.TaskStatus Status { get; set; }
}
