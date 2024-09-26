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
        CreateMap<TaskList, TaskListViewModel>().ReverseMap();
        CreateMap<TaskList, PaginatedTaskListViewModel>().ReverseMap();
        
        CreateMap<CreateTaskListEvent, TaskListViewModel>();
        CreateMap<CreateTaskItemEvent, TaskItemViewModel>();
    }
}