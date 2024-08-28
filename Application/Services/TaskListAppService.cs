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

    public TaskListViewModel CreateTaskList(CreateTaskListDto taskListDto)
    {
        var entityMap = _mapper.Map<TaskList>(taskListDto);
        
        _taskListRepository.Create(entityMap);
        _taskListRepository.SaveChanges();
        
        return _mapper.Map<TaskListViewModel>(entityMap);
    }

    public TaskListViewModel UpdateTaskList(UpdateTaskListDto taskListDto)
    {
        var entity = _taskListRepository.Query<TaskList>(x => x.Id == taskListDto.Id).FirstOrDefault();

        if (entity == null)
            throw new ApplicationException("not found");
        
        UpdateTaskList(ref entity, taskListDto);
        
        _taskListRepository.Update(entity);
        _taskListRepository.SaveChanges();
        
        return _mapper.Map<TaskListViewModel>(entity);
    }

    private void UpdateTaskList(ref TaskList oldtaskItem, UpdateTaskListDto newtaskItem)
    {
        oldtaskItem.SetTitle(newtaskItem.Title);
    }
}