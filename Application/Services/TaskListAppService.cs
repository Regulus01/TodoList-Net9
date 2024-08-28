using Application.Dto;
using Application.Dto.TaskList;
using Application.Interface;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;

namespace Application.Services;

public class TaskListAppService : ITaskListAppService
{
    private readonly ITaskListRepository _taskListRepository;
    private readonly IMapper _mapper;

    public TaskListAppService(ITaskListRepository taskListRepository, IMapper mapper)
    {
        _taskListRepository = taskListRepository;
        _mapper = mapper;
    }

    public TaskListViewModel CreateTaskList(CreateTaskListDto taskItem)
    {
        var entityMap = _mapper.Map<TaskList>(taskItem);
        
        _taskListRepository.Create(entityMap);
        _taskListRepository.SaveChanges();
        
        return _mapper.Map<TaskListViewModel>(entityMap);
    }

    public TaskItemViewModel UpdateTaskTitle(UpdateTaskListTitleDto taskItem)
    {
        var entity = _taskListRepository.Query<TaskList>(x => x.Id == taskItem.Id);

        if (entity == null)
            throw new ApplicationException("not found");
        
        var entityMap = _mapper.Map<TaskList>(taskItem);
        
        _taskListRepository.Update(entityMap);
        _taskListRepository.SaveChanges();
        
        return _mapper.Map<TaskItemViewModel>(entityMap);
    }
}