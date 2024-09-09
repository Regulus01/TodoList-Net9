using AutoMapper;
using Domain.Commands.Events;
using Domain.Entities;

namespace Application.Mapper.Mappings;

public class DomainToEventMappingProfile : Profile
{
    public DomainToEventMappingProfile()
    {
        CreateMap<TaskList, CreateTaskListEvent>();
        CreateMap<TaskItem, CreateTaskItemEvent>();
    }
}