using AutoMapper;
using CRUD_ASPNET.Application.DTO;
using CRUD_ASPNET.Models;
using CRUD_ASPNET.Pagination;
using CRUD_ASPNET.Repositories;

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

        public async Task<PagedList<ReadTaskDto>> GetAllTasksPaginated(int pageNumber, int pageSize)
        {
            var pagedTasks = await _repository.GetAllTasksPaginated(pageNumber, pageSize);

            var dtos = _mapper.Map<List<ReadTaskDto>>(pagedTasks.Data);

            return new PagedList<ReadTaskDto>(
                dtos,
                pagedTasks.TotalCount,
                pagedTasks.CurrentPage,
                pagedTasks.PageSize
            );
        }

        public async Task<ReadTaskDto?> GetTaskById(int id)
        {
            var task = await _repository.GetTaskById(id);

            if (task is null)
                throw new InvalidOperationException($"Task with id {id} not found.");

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

        public async Task<ReadTaskDto> UpdateTask(int id, UpdateTaskDTO dto)
        {

            _logger.LogInformation("Updating task with id: {Id}", id);

            var task = await _repository.GetTaskById(id);

            if (task is null)
            {
                throw new InvalidOperationException($"Task with id {id} not found.");
            }

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
