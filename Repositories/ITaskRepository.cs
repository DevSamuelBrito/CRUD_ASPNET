using CRUD_ASPNET.Application.DTO;
using CRUD_ASPNET.Models;
using System;

namespace CRUD_ASPNET.Repositories;

public interface ITaskRepository
{
    public Task<List<Tasks>> GetAllTasks();
    public Task<Tasks?> GetTaskById(int id);
    public Task<Tasks> AddTask(Tasks task);
    public Task<Tasks> UpdateTask(Tasks task);
    public Task DeleteTask(int id);
}
