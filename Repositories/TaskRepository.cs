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


    public async Task<List<Tasks>> GetAllTasks()
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


    public async Task AddTask(Tasks task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTask(Tasks task)
    {

        var existingTask = await _context.Tasks.FindAsync(task.Id);

        if (existingTask is null)
            throw new InvalidOperationException($"Task with id {task.Id} not found.");

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.UpdatedAt = DateTime.UtcNow;

        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public Task DeleteTask(Tasks task)
    {
        _context.Tasks.Remove(task);
        return _context.SaveChangesAsync();
    }

}


//todo: entender o metodo update e se precisa instalar algum Mapper, abrir PR no github e criar camada de servico (service) entre controller e repository, criar camada de controller, 