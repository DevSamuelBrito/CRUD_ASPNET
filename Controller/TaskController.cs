using CRUD_ASPNET.Application.DTO;
using CRUD_ASPNET.Services;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {

            try
            {
                var task = await _service.GetTaskById(id);
                return Ok(task);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }

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
            try
            {
                var updatedTask = await _service.UpdateTask(id, updateTaskDTO);
                return Ok(updatedTask);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                await _service.DeleteTask(id);
                return NoContent();  
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });  
            }
        }
    }
}