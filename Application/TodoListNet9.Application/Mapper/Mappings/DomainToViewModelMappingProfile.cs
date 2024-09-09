using Application.ViewModels.TaskItem;
using Application.ViewModels.TaskList;
using AutoMapper;
using Domain.Commands.Events;
using Domain.Entities;

namespace Application.Mapper.Mappings;

public class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile()
    {
        CreateMap<TaskItem, TaskItemViewModel>().ReverseMap();
        
        //Events
        CreateMap<CreateTaskListEvent, TaskListViewModel>();
        CreateMap<CreateTaskItemEvent, TaskItemViewModel>();
    }
}