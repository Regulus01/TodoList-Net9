using Application.Dto.TaskList;
using Application.Interface;
using Application.ViewModels.TaskList;
using AutoMapper;
using Domain.Commands;
using Domain.Commands.Events;
using Domain.Entities;
using Domain.Interface;
using Domain.Interface.Command.Interface;
using Domain.Resourcers;
using Infra.CrossCutting.Command.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

/// <inheritdoc />
public class TaskListAppService : ITaskListAppService
{
    private readonly ITaskListRepository _taskListRepository;
    private readonly IMapper _mapper;
    private readonly INotify _notify;
    private readonly ICommandInvoker _command;

    public TaskListAppService(ITaskListRepository taskListRepository, IMapper mapper, INotify notify, ICommandInvoker command)
    {
        _taskListRepository = taskListRepository;
        _mapper = mapper; 
        _notify = notify;
        _command = command;
    }

    /// <inheritdoc />
    public TaskListViewModel? Create(CreateTaskListDto? taskListDto)
    {
        if (taskListDto == null)
        {
            _notify.NewNotification(ErrorMessage.NULL_FIELDS.Code, ErrorMessage.NULL_FIELDS.Message);
            return null;
        }
        
        var command = _mapper.Map<CreateTaskListCommand>(taskListDto);
        
        var result = _command.Execute<CreateTaskListCommand, CreateTaskListEvent?>(command);
        
        return _mapper.Map<TaskListViewModel>(result);
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

        if (!validationResult.IsValid)
        {
            _notify.NewNotification(validationResult.Erros);
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
}