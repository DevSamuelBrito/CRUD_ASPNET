using CRUD_ASPNET.Application.DTO;
using CRUD_ASPNET.Models;

namespace CRUD_ASPNET.Services
{
    public interface ITaskService
    {
        public Task<IEnumerable<ReadTaskDto>> GetAllTasks();
        public Task<ReadTaskDto?> GetTaskById(int id);
        public Task<ReadTaskDto> CreateTask(CreateTaskDTO createTaskDto);
        public Task<ReadTaskDto> UpdateTask(int id, UpdateTaskDTO updateTaskDTO);
        public Task DeleteTask(int id);
    }
}
