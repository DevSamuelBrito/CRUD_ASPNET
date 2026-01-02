using CRUD_ASPNET.Application.DTO;
using CRUD_ASPNET.Domain.Entities;
using CRUD_ASPNET.Infra.Repositories.Interfaces;
using CRUD_ASPNET.Services;
using Microsoft.Extensions.Logging;
using Moq;
using TaskStatus = CRUD_ASPNET.Domain.Entities.TaskStatus;

namespace CRUD_ASPNET.Tests.Services
{
    public class TaskServiceTests
    {
        private readonly TaskService _taskService;
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly Mock<AutoMapper.IMapper> _mapperMock;
        private readonly ILogger<TaskService> _loggerMock;

        public TaskServiceTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _loggerMock = new Mock<ILogger<TaskService>>().Object;
            _mapperMock = new Mock<AutoMapper.IMapper>();
            _taskService = new TaskService(_taskRepositoryMock.Object, _mapperMock.Object, _loggerMock);
        }

        [Fact]
        public async Task GetTaskById_WhenTaskExists_ReturnsDto()
        {
            // Arrange
            var taskId = 1;
            var task = new Tasks
            {
                Id = taskId,
                Title = "Test Task",
                Description = "Test Description",
                Status = TaskStatus.ToDo,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var taskDto = new ReadTaskDto
            {
                Id = taskId,
                Title = "Test Task",
                Description = "Test Description",
                Status = TaskStatus.ToDo
            };

            // Configura o mock do repository para retornar a task
            _taskRepositoryMock
                .Setup(repo => repo.GetTaskById(taskId))
                .ReturnsAsync(task);

            // Configura o mock do mapper para converter Task em ReadTaskDto
            _mapperMock
                .Setup(mapper => mapper.Map<ReadTaskDto>(task))
                .Returns(taskDto);

            // Act
            var result = await _taskService.GetTaskById(taskId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(taskId, result.Id);
            Assert.Equal("Test Task", result.Title);
            Assert.Equal("Test Description", result.Description);
            Assert.Equal(TaskStatus.ToDo, result.Status);

            // Verifica que os métodos foram chamados
            _taskRepositoryMock.Verify(repo => repo.GetTaskById(taskId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<ReadTaskDto>(task), Times.Once);
        }

        [Fact]
        public async Task GetTaskById_WhenTaskDoesNotExist_ReturnsNull()
        {
            // Arrange
            var taskId = 999;

            _taskRepositoryMock
                .Setup(repo => repo.GetTaskById(taskId))
                .ReturnsAsync((Tasks?)null);

            // Act
            var result = await _taskService.GetTaskById(taskId);

            // Assert
            Assert.Null(result);
            _taskRepositoryMock.Verify(repo => repo.GetTaskById(taskId), Times.Once);
        }
    }
}
