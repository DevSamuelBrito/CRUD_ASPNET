using CRUD_ASPNET.Application.DTO;
using CRUD_ASPNET.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_ASPNET.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _service.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetAllTasks([FromQuery] GetParametersDTO parameters)
        {

            if (parameters.PageSize > 100)
            {
                return BadRequest(new { error = "PageSize cannot be greater than 100." });
            }

            var tasks = await _service.GetAllTasksPaginated(parameters.PageNumber, parameters.PageSize, parameters.title, parameters.status);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _service.GetTaskById(id);

            if (task is null)
                return NotFound($"Task with id {id} not found.");

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] CreateTaskDTO createTaskDTO)
        {
            var createdTask = await _service.CreateTask(createTaskDTO);

            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDTO updateTaskDTO)
        {
            var updatedTask = await _service.UpdateTask(id, updateTaskDTO);

            if(updatedTask is null)
                return NotFound($"Task with id {id} not found.");

            return Ok(updatedTask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _service.DeleteTask(id);
            return NoContent();
        }
    }
}