using CRUD_ASPNET.Models;

namespace CRUD_ASPNET.Application.DTO
{
    public class GetParametersDTO
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? title { get; set; }
        public Models.TaskStatus? status { get; set; }
    }
}
