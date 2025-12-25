using CRUD_ASPNET.Application.DTO;
using CRUD_ASPNET.Configuration.Context;
using CRUD_ASPNET.Models;
using CRUD_ASPNET.Pagination;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CRUD_ASPNET.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<Tasks>> GetAllTasksPaginated(int pageNumber, int pageSize)
    {
        var query = _context.Tasks.AsQueryable();

        var count = await query.CountAsync();  
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<Tasks>(items, count, pageNumber, pageSize);
    }

    public async Task<Tasks?> GetTaskById(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task<Tasks> AddTask(Tasks task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<Tasks> UpdateTask(Tasks task)
    {
        _context.Update(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task DeleteTask(int id)
    {
        var entity = await _context.Tasks.FindAsync(id);

        if (entity is null) throw new InvalidOperationException($"Task with id {id} not found.");

        _context.Tasks.Remove(entity);

        await _context.SaveChangesAsync();
    }

}