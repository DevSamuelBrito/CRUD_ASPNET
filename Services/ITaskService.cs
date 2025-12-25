using CRUD_ASPNET.Application.DTO;
using CRUD_ASPNET.Pagination;

namespace CRUD_ASPNET.Services
{
    public interface ITaskService
    {
         Task<List<ReadTaskDto>> GetAllTasks();
        public Task<PagedList<ReadTaskDto>> GetAllTasksPaginated(int pageNumber, int pageSize);
        public Task<ReadTaskDto?> GetTaskById(int id);
        public Task<ReadTaskDto> CreateTask(CreateTaskDTO createTaskDto);
        public Task<ReadTaskDto> UpdateTask(int id, UpdateTaskDTO updateTaskDTO);
        public Task DeleteTask(int id);
    }
}
