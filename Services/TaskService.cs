using AutoMapper;
using CRUD_ASPNET.Application.DTO;
using CRUD_ASPNET.Models;
using CRUD_ASPNET.Repositories;

namespace CRUD_ASPNET.Services
{
    public class TaskService : ITaskService
    {

        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadTaskDto>> GetAllTasks()
        {
            var tasks = await _repository.GetAllTasks();
            return _mapper.Map<IEnumerable<ReadTaskDto>>(tasks);
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
            var task = _mapper.Map<Tasks>(dto);

            var createdTask = await _repository.AddTask(task);

            return _mapper.Map<ReadTaskDto>(createdTask);
        }

        public async Task<ReadTaskDto> UpdateTask(int id, UpdateTaskDTO dto)
        {
            var task = await _repository.GetTaskById(id);

            if (task is null)
            {
                throw new InvalidOperationException($"Task with id {id} not found.");
            }

            _mapper.Map(dto, task);

            var updated = await _repository.UpdateTask(task);

            return _mapper.Map<ReadTaskDto>(updated);
        }

        public async Task DeleteTask(int id)
        {
            await _repository.DeleteTask(id);
        }

    }

}
