using AutoMapper;
using CRUD_ASPNET.Application.DTO;
using CRUD_ASPNET.Domain.Entities;

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
