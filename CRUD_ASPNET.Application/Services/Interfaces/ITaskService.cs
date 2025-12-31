using CRUD_ASPNET.Application.DTO;
using CRUD_ASPNET.Infra.Pagination;

namespace CRUD_ASPNET.Application.Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<ReadTaskDto>> GetAllTasks();
        public Task<PagedList<ReadTaskDto>> GetAllTasksPaginated(int pageNumber, int pageSize, string? title, Domain.Entities.TaskStatus? status);
        public Task<ReadTaskDto?> GetTaskById(int id);
        public Task<ReadTaskDto> CreateTask(CreateTaskDTO createTaskDto);
        public Task<ReadTaskDto?> UpdateTask(int id, UpdateTaskDTO updateTaskDTO);
        public Task DeleteTask(int id);
    }
}
