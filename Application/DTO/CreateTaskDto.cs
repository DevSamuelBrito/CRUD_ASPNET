using CRUD_ASPNET.Models;
using System.ComponentModel.DataAnnotations;

namespace CRUD_ASPNET.Application.DTO
{
    public class CreateTaskDTO
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 200 characters")]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required")]
        [EnumDataType(typeof(CRUD_ASPNET.Models.TaskStatus), ErrorMessage = "Status must be ToDo (1), Doing (2), or Done (3)")]
        public CRUD_ASPNET.Models.TaskStatus Status { get; set; } = CRUD_ASPNET.Models.TaskStatus.ToDo;
    }
}
