using System.ComponentModel.DataAnnotations;
using CRUD_ASPNET.Models;

namespace CRUD_ASPNET.Application.DTO
{
    public class UpdateTaskDTO
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 200 characters")]
        public string? Title { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [EnumDataType(typeof(Models.TaskStatus), ErrorMessage = "Status must be ToDo (1), Doing (2), or Done (3)")]
        public Models.TaskStatus? Status { get; set; }
    }
}
