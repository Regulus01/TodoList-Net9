using Application.Dto.TaskItem;
using Application.Dto.TaskList;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.Mappings;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<TaskItem, CreatePartialTaskItemDto>().ReverseMap();
        CreateMap<TaskList, CreateTaskListDto>().ReverseMap();
    }
}