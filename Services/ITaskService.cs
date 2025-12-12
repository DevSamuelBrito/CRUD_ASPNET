using CRUD_ASPNET.Application.DTO;
using CRUD_ASPNET.Models;

namespace CRUD_ASPNET.Services
{
    public interface ITaskService
    {
        public Task<IEnumerable<ReadTaskDto>> GetAllTasksAsync();
        public Task<ReadTaskDto?> GetTaskByIdAsync(int id);
        public Task<ReadTaskDto> CreateTaskAsync(CreateTaskDTO createTaskDto);
        public Task<UpdateTaskDTO> UpdateTaskASync(int id, UpdateTaskDTO updateTaskDTO);
        public Task<ReadTaskDto> DeleteTask(int id);
    }
}
