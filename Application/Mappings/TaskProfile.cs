using AutoMapper;
using CRUD_ASPNET.Models;
using CRUD_ASPNET.Application.DTO;

namespace CRUD_ASPNET.Application.Mappings
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Tasks, ReadTaskDto>();
            CreateMap<CreateTaskDTO, Tasks>();
            CreateMap<UpdateTaskDTO, Tasks>();
        }
    }
}
