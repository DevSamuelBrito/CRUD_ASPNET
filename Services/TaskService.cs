using AutoMapper;
using CRUD_ASPNET.Application.DTO;
using CRUD_ASPNET.Repositories;

namespace CRUD_ASPNET.Services
{
    public class TaskService : ITaskService
    {

        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository repositoty, IMapper mapper)
        {
            _repository = repositoty;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadTaskDto>> GetAllTasksAsync()
        {
            var tasks = await _repository.GetAllTasks();
            return _mapper.Map<IEnumerable<ReadTaskDto>>(tasks);
        }

        public async Task<ReadTaskDto?> GetTaskByIdAsync(int id)
        {
            var task = await _repository.GetTaskById(id);
            return task is null ? null : _mapper.Map<ReadTaskDto>(task);
        }

        public async Task<ReadTaskDto> CreateTaskAsync(CreateTaskDTO createTaskDto)
        {
            var task = _mapper.Map<Models.Tasks>(createTaskDto);
            var createdTask = await _repository.AddTask(task);
            return _mapper.Map<ReadTaskDto>(createdTask);
        }

        public async Task<UpdateTaskDTO> UpdateTaskASync(int id, UpdateTaskDTO updateTaskDTO)
        {
            var task = await _repository.GetTaskById(id);
            if (task is null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            var updatedTask = _mapper.Map<Models.Tasks>(updateTaskDTO);
            await _repository.UpdateTask(id, updatedTask);

            return updateTaskDTO;
        }

        public async Task<ReadTaskDto> DeleteTask(int id)
        {

            var task = await _repository.GetTaskById(id);

            if (task is null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            await _repository.DeleteTask(id);

            return _mapper.Map<ReadTaskDto>(task);
        }
      
    }

}
