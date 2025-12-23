using CRUD_ASPNET.Models;
using CRUD_ASPNET.Pagination;

namespace CRUD_ASPNET.Repositories;

public interface ITaskRepository
{
    public Task<PagedList<Tasks>> GetAllTasksPaginated(int pageNumber, int pageSize);
    public Task<Tasks?> GetTaskById(int id);
    public Task<Tasks> AddTask(Tasks task);
    public Task<Tasks> UpdateTask(Tasks task);
    public Task DeleteTask(int id);
}
