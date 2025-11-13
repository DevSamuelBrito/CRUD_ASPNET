using CRUD_ASPNET.Models;
using System;

namespace CRUD_ASPNET.Repositories;

public interface ITaskRepository
{
    public Task<List<Tasks>> GetAllTasks();
    public Task<Tasks> GetTaskById(int id);
    public void AddTask();
    public void UpdateTask();
    public void DeleteTask();
}
