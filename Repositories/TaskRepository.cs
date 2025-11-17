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


    public async Task AddTask(Tasks dto)
    {
        var task = _mapper.Map<Tasks>(dto);

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTask(int id, Tasks dto)
    {

        var existingTask = await _context.Tasks.FindAsync(id);

        if (existingTask is null)
            throw new InvalidOperationException($"Task with id {id} not found.");

        _mapper.Map(dto, existingTask);

        await _context.SaveChangesAsync();
    }

    public Task DeleteTask(Tasks task)
    {
        _context.Tasks.Remove(task);
        return _context.SaveChangesAsync();
    }

}


//todo, abrir PR no github e criar camada de servico (service) entre controller e repository, criar camada de controller, 