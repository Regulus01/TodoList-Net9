using Application.ViewModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.Mappings;

public class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile()
    {
        CreateMap<TaskItem, TaskItemViewModel>().ReverseMap();
        CreateMap<TaskList, TaskListViewModel>().ReverseMap();
    }
}