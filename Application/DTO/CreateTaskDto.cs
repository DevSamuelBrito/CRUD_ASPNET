using CRUD_ASPNET.Models;

namespace CRUD_ASPNET.Application.DTO
{
    public class CreateTaskDTO
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public CRUD_ASPNET.Models.TaskStatus Status { get; set; } = CRUD_ASPNET.Models.TaskStatus.ToDo;
    }
}
