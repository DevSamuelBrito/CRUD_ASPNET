namespace CRUD_ASPNET.Application.DTO
{
    public class UpdateTaskDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public CRUD_ASPNET.Models.TaskStatus? Status { get; set; }
    }
}
