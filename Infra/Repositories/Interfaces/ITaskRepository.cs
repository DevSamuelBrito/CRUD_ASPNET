using CRUD_ASPNET.Domain.Entities;

namespace CRUD_ASPNET.Infra.Repositories.Interfaces;

public interface ITaskRepository
{
    public Task<(List<Tasks>, int totalCount)> GetAllTasksPaginated(int pageNumber, int pageSize, string? title = null, Domain.Entities.TaskStatus? status = null);
    public Task<Tasks?> GetTaskById(int id);
    public Task<Tasks> AddTask(Tasks task);
    public Task<Tasks> UpdateTask(Tasks task);
    public Task DeleteTask(int id);
}
