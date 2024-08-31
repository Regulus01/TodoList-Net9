using Application.Dto.TaskItem;
using Application.Interface;
using Application.ViewModels.TaskItem;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Domain.Resourcers;
using Infra.CrossCutting.Notification.Interface;

namespace Application.Services;

/// <inheritdoc />
public class TaskItemAppService : ITaskItemAppService
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IMapper _mapper;
    private readonly INotify _notify;

    public TaskItemAppService(ITaskItemRepository taskItemRepository, IMapper mapper, INotify notify)
    {
        _taskItemRepository = taskItemRepository;
        _mapper = mapper;
        _notify = notify;
    }

    /// <inheritdoc />
    public TaskItemViewModel? CreateTaskItem(CreateTaskItemDto? dto)
    {
        if (dto == null)
        {
            _notify.NewNotification(ErrorMessage.NULL_FIELDS.Code, ErrorMessage.NULL_FIELDS.Message);
            return null;
        }
        
        var taskListExists = _taskItemRepository.Query<TaskList>(x => x.Id == dto.TaskListId).Any();

        if (!taskListExists)
        {
            _notify.NewNotification(ErrorMessage.TASK_LIST_NOT_EXIST.Code, ErrorMessage.TASK_LIST_NOT_EXIST.Message);
            return null;
        }
            
        var entity = new TaskItem(dto.Title, dto.Description, dto.DueDate, dto.TaskListId);

        var validationResult = entity.Validate();

        if (validationResult.isValid)
        {
            _notify.NewNotification(validationResult.erros);
            return null;
        }
        
        _taskItemRepository.Add(entity);

        if (!_taskItemRepository.SaveChanges())
        {
            _notify.NewNotification(ErrorMessage.SAVE_DATA.Code, ErrorMessage.SAVE_DATA.Message);
            return null;
        }

        return _mapper.Map<TaskItemViewModel>(entity);
    }

    /// <inheritdoc />
    public TaskItemViewModel? UpdateTaskItem(UpdateTaskItemDto taskItem)
    {
        var entity = _taskItemRepository.Query<TaskItem>(x => x.Id == taskItem.Id).FirstOrDefault();

        if (entity == null)
        {
            _notify.NewNotification(ErrorMessage.DATA_NOT_FOUND.Code, ErrorMessage.DATA_NOT_FOUND.Message);
            return null;
        }

        entity.Update(taskItem.Title, taskItem.Description, taskItem.DueDate, taskItem.IsCompleted);

        var validationResult = entity.Validate();

        if (validationResult.isValid)
        {
            _notify.NewNotification(validationResult.erros);
            return null;
        }

        _taskItemRepository.Update(entity);
        
        if (!_taskItemRepository.SaveChanges())
        {
            _notify.NewNotification(ErrorMessage.DATA_NOT_FOUND.Code, ErrorMessage.DATA_NOT_FOUND.Message);
            return null;
        }

        return _mapper.Map<TaskItemViewModel>(entity);
    }
}