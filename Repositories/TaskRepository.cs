using CRUD_ASPNET.Configuration.Context;
using CRUD_ASPNET.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CRUD_ASPNET.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task <List<Tasks>> GetAllTasks()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<Tasks> GetTaskById(int id)
    {

        var task = await _context.Tasks.FindAsync(id);

        if (task is null)
            throw new InvalidOperationException($"Task with id {id} not found.");
        return task;
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
