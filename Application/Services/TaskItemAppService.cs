using Application.Dto.TaskItem;
using Application.Interface;
using Application.ViewModels;
using Application.ViewModels.TaskItem;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Domain.Resourcers;

namespace Application.Services;

public class TaskItemAppService : ITaskItemAppService
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IMapper _mapper;

    public TaskItemAppService(ITaskItemRepository taskItemRepository, IMapper mapper)
    {
        _taskItemRepository = taskItemRepository;
        _mapper = mapper;
    }

    public TaskItemViewModel CreateTaskItem(CreateTaskItemDto dto)
    {
        var taskList = _taskItemRepository.Query<TaskList>(x => dto.TaskListId == x.Id).FirstOrDefault();

        if (taskList == null)
            throw new ApplicationException(ErrorMessage.NOT_FOUND);
        
        var entity = new TaskItem(dto.Title, dto.Description, dto.DueDate, dto.TaskListId);

        _taskItemRepository.Add(entity);

        if (!_taskItemRepository.SaveChanges())
            throw new Exception(ErrorMessage.ERROR_TO_SAVE);

        return _mapper.Map<TaskItemViewModel>(entity);
    }

    public TaskItemViewModel UpdateTaskItem(UpdateTaskItemDto taskItem)
    {
        var entity = _taskItemRepository.Query<TaskItem>(x => x.Id == taskItem.Id).FirstOrDefault();

        if (entity == null)
            throw new ApplicationException(ErrorMessage.NOT_FOUND);

        entity.Update(taskItem.Title, taskItem.Description, taskItem.DueDate, taskItem.IsCompleted);

        _taskItemRepository.Update(entity);
        
        if (!_taskItemRepository.SaveChanges())
            throw new Exception(ErrorMessage.ERROR_TO_UPDATE);

        return _mapper.Map<TaskItemViewModel>(entity);
    }
}