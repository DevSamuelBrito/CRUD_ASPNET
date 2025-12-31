using CRUD_ASPNET.Configuration.Context;
using CRUD_ASPNET.Domain.Entities;
using CRUD_ASPNET.Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRUD_ASPNET.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(List<Tasks>, int totalCount)> GetAllTasksPaginated(int pageNumber, int pageSize, string? title = null, Domain.Entities.TaskStatus? status = null)
    {
        var query = _context.Tasks
        .Where(t => (title == null || t.Title.Contains(title, StringComparison.CurrentCultureIgnoreCase)) &&
                (status == null || t.Status == status));

        var count = await query.CountAsync();

        var items = await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, count);
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