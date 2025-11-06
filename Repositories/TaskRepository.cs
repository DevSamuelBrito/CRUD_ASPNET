using System;
using CRUD_ASPNET.Configuration.Context;

namespace CRUD_ASPNET.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }


    public void GetAllTasks()
    {

    }

    public void GetTaskById()
    {

    }

    public void AddTask()
    {

    }

    public void UpdateTask()
    {

    }
    
    public void DeleteTask()
    {
        
    }
}
