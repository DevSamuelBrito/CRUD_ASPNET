using CRUD_ASPNET.Application.DTO;
using CRUD_ASPNET.Models;
using System;

namespace CRUD_ASPNET.Repositories;

public interface ITaskRepository
{
    public Task<List<ReadTaskDto>> GetAllTasks();
    public Task<ReadTaskDto> GetTaskById(int id);
    public Task<ReadTaskDto> AddTask(Tasks task);
    public Task<ReadTaskDto> UpdateTask(int id, Tasks dto);
    public Task DeleteTask(Tasks id);
}
