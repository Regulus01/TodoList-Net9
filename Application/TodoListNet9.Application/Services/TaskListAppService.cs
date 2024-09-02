using Application.Dto.TaskList;
using Application.Interface;
using Application.ViewModels.TaskList;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Domain.Resourcers;
using Infra.CrossCutting.Notification.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

/// <inheritdoc />
public class TaskListAppService : ITaskListAppService
{
    private readonly ITaskListRepository _taskListRepository;
    private readonly IMapper _mapper;
    private readonly INotify _notify;

    public TaskListAppService(ITaskListRepository taskListRepository, IMapper mapper, INotify notify)
    {
        _taskListRepository = taskListRepository;
        _mapper = mapper;
        _notify = notify;
    }

    /// <inheritdoc />
    public TaskListViewModel? Create(CreateTaskListDto? taskListDto)
    {
        if (taskListDto == null)
        {
            _notify.NewNotification(ErrorMessage.NULL_FIELDS.Code, ErrorMessage.NULL_FIELDS.Message);
            return null;
        }
        
        var entity = CreateTaskListEntity(taskListDto);

        if (_notify.HasNotifications())
        {
            return null;
        }
        
        var validationResult = entity.Validate();

        if (validationResult.isValid)
        {
            _notify.NewNotification(validationResult.erros);
            return null;
        }

        _taskListRepository.Add(entity);

        if (!_taskListRepository.SaveChanges())
        {
            _notify.NewNotification(ErrorMessage.SAVE_DATA.Code, ErrorMessage.SAVE_DATA.Message);
            return null;
        }
        
        return _mapper.Map<TaskListViewModel>(entity);
    }

    /// <inheritdoc />
    public TaskListViewModel? Get(Guid? id)
    {
        var taskList = _taskListRepository.Query<TaskList>(x => x.Id == id,
            y => y.Include(i => i.TaskItems)!).FirstOrDefault();

        if (taskList == null)
        {
            _notify.NewNotification(ErrorMessage.DATA_NOT_FOUND.Code, ErrorMessage.DATA_NOT_FOUND.Message);
            return null;
        }

        return _mapper.Map<TaskListViewModel>(taskList);
    }

    /// <inheritdoc />
    public TaskListViewModel? Update(UpdateTaskListDto taskListDto)
    {
        var entity = _taskListRepository.Query<TaskList>(x => x.Id == taskListDto.Id).FirstOrDefault();

        if (entity == null)
        {
            _notify.NewNotification(ErrorMessage.DATA_NOT_FOUND.Code, ErrorMessage.DATA_NOT_FOUND.Message);
            return null;
        }
        
        entity.Update(taskListDto.Title);

        var validationResult = entity.Validate();

        if (validationResult.isValid)
        {
            _notify.NewNotification(validationResult.erros);
            return null;
        }
        
        _taskListRepository.Update(entity);

        if (!_taskListRepository.SaveChanges())
        {
            _notify.NewNotification(ErrorMessage.UPDATE_DATA.Code, ErrorMessage.UPDATE_DATA.Message);
            return null;
        }

        return _mapper.Map<TaskListViewModel>(entity);
    }

    /// <inheritdoc />
    public void Delete(Guid? id)
    {
        var taskList = _taskListRepository.Query<TaskList>(x => x.Id == id,
            y => y.Include(i => i.TaskItems)!).FirstOrDefault();

        if (taskList == null)
        {
            _notify.NewNotification(ErrorMessage.DATA_NOT_FOUND.Code, ErrorMessage.DATA_NOT_FOUND.Message);
            return;
        }

        ValidateDeleteTaskListDto(taskList);

        if (_notify.HasNotifications())
        {
            return;
        }

        _taskListRepository.Delete(taskList);

        if (!_taskListRepository.SaveChanges())
        {
            _notify.NewNotification(ErrorMessage.DELETE_DATA.Code, ErrorMessage.DELETE_DATA.Message);
        }
    }

    private void ValidateDeleteTaskListDto(TaskList? taskList)
    {
        if (taskList == null)
        {
            _notify.NewNotification(ErrorMessage.DELETE_DATA.Code, ErrorMessage.DELETE_DATA.Message);
            return;
        }
        
        if (taskList.TaskItems != null)
        {
            _notify.NewNotification(ErrorMessage.LINKED_TASKITEM.Code, ErrorMessage.LINKED_TASKITEM.Message);
            return;
        }
    }

    private TaskList CreateTaskListEntity(CreateTaskListDto taskListDto)
    {
        var taskItens = new List<TaskItem>();

        if (taskListDto.TaskItems != null && taskListDto.TaskItems.Any())
        {
            foreach (var taskItemDto in taskListDto.TaskItems)
            {
                var taskItem = new TaskItem(taskItemDto.Title, taskItemDto.Description, taskItemDto.DueDate);
                
                var validationResult = taskItem.Validate();

                if (validationResult.isValid)
                {
                    _notify.NewNotification(validationResult.erros);
                    continue;
                }
                
                taskItens.Add(taskItem);
            }
        }

        var entity = new TaskList(taskListDto.Title, taskItens);

        return entity;
    }
}