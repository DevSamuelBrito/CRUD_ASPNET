namespace CRUD_ASPNET.Application.DTO
{
    public class UpdateTaskDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public CRUD_ASPNET.Models.TaskStatus? Status { get; set; }
    }
}
