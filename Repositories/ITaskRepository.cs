using System;

namespace CRUD_ASPNET.Repositories;

public interface ITaskRepository
{
    public void GetAllTasks();
    public void GetTaskById();
    public void AddTask();
    public void UpdateTask();
    public void DeleteTask();
}
