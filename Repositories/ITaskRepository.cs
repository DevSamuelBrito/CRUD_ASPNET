using CRUD_ASPNET.Models;

namespace CRUD_ASPNET.Repositories;

public interface ITaskRepository
{
    public Task<(List<Tasks>, int totalCount)> GetAllTasksPaginated(int pageNumber, int pageSize);
    public Task<Tasks?> GetTaskById(int id);
    public Task<Tasks> AddTask(Tasks task);
    public Task<Tasks> UpdateTask(Tasks task);
    public Task DeleteTask(int id);
}
