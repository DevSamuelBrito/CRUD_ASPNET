using AutoMapper;
using CRUD_ASPNET.Application.DTO;
using CRUD_ASPNET.Application.Services.Interfaces;
using CRUD_ASPNET.Domain.Entities;
using CRUD_ASPNET.Infra.Pagination;
using CRUD_ASPNET.Infra.Repositories.Interfaces;

namespace CRUD_ASPNET.Services
{
    public class TaskService : ITaskService
    {

        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository repository, IMapper mapper, ILogger<TaskService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ReadTaskDto>> GetAllTasks()
        {
            var (tasks, _) = await _repository.GetAllTasksPaginated(1, int.MaxValue);

            return _mapper.Map<List<ReadTaskDto>>(tasks);
        }

        public async Task<PagedList<ReadTaskDto>> GetAllTasksPaginated(int pageNumber, int pageSize, string? title, Domain.Entities.TaskStatus? status)
        {
            var (tasks, totalCount) = await _repository.GetAllTasksPaginated(pageNumber, pageSize, title, status);

            var dtos = _mapper.Map<List<ReadTaskDto>>(tasks);

            return new PagedList<ReadTaskDto>(dtos, totalCount, pageNumber, pageSize);
        }

        public async Task<ReadTaskDto?> GetTaskById(int id)
        {
            var task = await _repository.GetTaskById(id);

            if (task is null)
                return null;

            return _mapper.Map<ReadTaskDto>(task);
        }

        public async Task<ReadTaskDto> CreateTask(CreateTaskDTO dto)
        {

            _logger.LogInformation("Creating a new task with title: {Title}", dto.Title);

            var task = _mapper.Map<Tasks>(dto);

            var createdTask = await _repository.AddTask(task);

            _logger.LogInformation("Task created successfully. ID: {Id}", createdTask.Id);

            return _mapper.Map<ReadTaskDto>(createdTask);
        }

        public async Task<ReadTaskDto?> UpdateTask(int id, UpdateTaskDTO dto)
        {

            _logger.LogInformation("Updating task with id: {Id}", id);

            var task = await _repository.GetTaskById(id);

            if (task is null)
                return null;

            _mapper.Map(dto, task);

            var updated = await _repository.UpdateTask(task);

            _logger.LogInformation("Task with id: {Id} updated successfully", id);

            return _mapper.Map<ReadTaskDto>(updated);
        }

        public async Task DeleteTask(int id)
        {
            _logger.LogInformation("Deleting task with ID: {Id}", id);

            await _repository.DeleteTask(id);

            _logger.LogInformation("Task with ID: {Id} deleted successfully", id);
        }

    }

}
