using Application.Dto.TaskItem;
using Application.Interface;
using Application.ViewModels.TaskItem;
using AutoMapper;
using Domain.Bus;
using Domain.Entities;
using Domain.Interface.Repository;
using Domain.Resourcers;

namespace Application.Services;

/// <inheritdoc />
public class TaskItemAppService : ITaskItemAppService
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IMapper _mapper;
    private readonly Bus _bus;

    public TaskItemAppService(ITaskItemRepository taskItemRepository, IMapper mapper, Bus bus)
    {
        _taskItemRepository = taskItemRepository;
        _mapper = mapper;
        _bus = bus;
    }

    /// <inheritdoc />
    public TaskItemViewModel? CreateTaskItem(CreateTaskItemDto? dto)
    {
        if (dto == null)
        {
            _bus.Notify.NewNotification(ErrorMessage.NULL_FIELDS.Code, ErrorMessage.NULL_FIELDS.Message);
            return null;
        }
        
        var taskListExists = _taskItemRepository.Query<TaskList>(x => x.Id == dto.TaskListId).Any();

        if (!taskListExists)
        {
            _bus.Notify.NewNotification(ErrorMessage.TASK_LIST_NOT_EXIST.Code, ErrorMessage.TASK_LIST_NOT_EXIST.Message);
            return null;
        }
            
        var entity = new TaskItem(dto.Title, dto.Description, dto.DueDate, dto.TaskListId);

        var validationResult = entity.Validate();

        if (!validationResult.IsValid)
        {
            _bus.Notify.NewNotification(validationResult.Erros);
            return null;
        }
        
        _taskItemRepository.Add(entity);

        if (!_taskItemRepository.SaveChanges())
        {
            _bus.Notify.NewNotification(ErrorMessage.SAVE_DATA.Code, ErrorMessage.SAVE_DATA.Message);
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
            _bus.Notify.NewNotification(ErrorMessage.DATA_NOT_FOUND.Code, ErrorMessage.DATA_NOT_FOUND.Message);
            return null;
        }

        entity.Update(taskItem.Title, taskItem.Description, taskItem.DueDate, taskItem.IsCompleted);

        var validationResult = entity.Validate();

        if (!validationResult.IsValid)
        {
            _bus.Notify.NewNotification(validationResult.Erros);
            return null;
        }

        _taskItemRepository.Update(entity);
        
        if (!_taskItemRepository.SaveChanges())
        {
            _bus.Notify.NewNotification(ErrorMessage.DATA_NOT_FOUND.Code, ErrorMessage.DATA_NOT_FOUND.Message);
            return null;
        }

        return _mapper.Map<TaskItemViewModel>(entity);
    }
    
    /// <inheritdoc />
    public TaskItemViewModel? Get(Guid? id)
    {
        var taskItem = _taskItemRepository.Query<TaskItem>(x => x.Id == id).FirstOrDefault();

        if (taskItem == null)
        {
            _bus.Notify.NewNotification(ErrorMessage.DATA_NOT_FOUND.Code, ErrorMessage.DATA_NOT_FOUND.Message);
            return null;
        }

        return _mapper.Map<TaskItemViewModel>(taskItem);
    }
    
    /// <inheritdoc />
    public void Delete(Guid? id)
    {
        var taskItem = _taskItemRepository.Query<TaskItem>(x => x.Id == id).FirstOrDefault();

        if (taskItem == null)
        {
            _bus.Notify.NewNotification(ErrorMessage.DATA_NOT_FOUND.Code, ErrorMessage.DATA_NOT_FOUND.Message);
            return;
        }
        
        _taskItemRepository.Delete(taskItem);

        if (!_taskItemRepository.SaveChanges())
        {
            _bus.Notify.NewNotification(ErrorMessage.DELETE_DATA.Code, ErrorMessage.DELETE_DATA.Message);
        }
    }
}