using Application.ViewModels;
using Application.ViewModels.TaskItem;
using Application.ViewModels.TaskList;
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