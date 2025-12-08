using CRUD_ASPNET.Application.DTO;
using CRUD_ASPNET.Configuration.Context;
using CRUD_ASPNET.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CRUD_ASPNET.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    private readonly AutoMapper.IMapper _mapper;

    public TaskRepository(AppDbContext context, AutoMapper.IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<List<ReadTaskDto>> GetAllTasks()
    {
        var tasks = await _context.Tasks.ToListAsync();
        return _mapper.Map<List<ReadTaskDto>>(tasks);
    }

    public async Task<ReadTaskDto> GetTaskById(int id)
    {

        var task = await _context.Tasks.FindAsync(id);

        if (task is null)
            throw new InvalidOperationException($"Task with id {id} not found.");
        return _mapper.Map<ReadTaskDto>(task);
    }


    public async Task<ReadTaskDto> AddTask(Tasks dto)
    {
        var task = _mapper.Map<Tasks>(dto);

        _context.Tasks.Add(task);

        await _context.SaveChangesAsync();

        return _mapper.Map<ReadTaskDto>(task);
    }

    public async Task<ReadTaskDto> UpdateTask(int id, Tasks dto)
    {

        var existingTask = await _context.Tasks.FindAsync(id);

        if (existingTask is null)
            throw new InvalidOperationException($"Task with id {id} not found.");

        _mapper.Map(dto, existingTask);

        //await _context.Tasks.

        await _context.SaveChangesAsync();

        return _mapper.Map<ReadTaskDto>(existingTask);
    }

    public async Task DeleteTask(int id)
    {
        var entity = await _context.Tasks.FindAsync(id);

        if (entity is null) throw new InvalidOperationException($"Task with id {id} not found.");

        _context.Tasks.Remove(entity);

        await _context.SaveChangesAsync();
    }

}