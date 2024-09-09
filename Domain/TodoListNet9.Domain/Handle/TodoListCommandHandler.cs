using AutoMapper;
using Domain.Commands;
using Domain.Commands.Events;
using Domain.Entities;
using Domain.Interface;
using Domain.Interface.Command.Interface;
using Domain.Resourcers;
using Infra.CrossCutting.Command.Interface.Handler;

namespace Domain.Handle;

public class TodoListCommandHandler : ICommandHandlerWithEvent<CreateTaskListCommand, CreateTaskListEvent?>
{
    private readonly ITaskListRepository _taskListRepository;
    private readonly INotify _notify;
    private readonly IMapper _mapper;


    public TodoListCommandHandler(ITaskListRepository taskListRepository, INotify notify, IMapper mapper)
    {
        _taskListRepository = taskListRepository;
        _notify = notify;
        _mapper = mapper;
    }
    
    public CreateTaskListEvent? Handle(CreateTaskListCommand command)
    {
        var entity = CreateTaskListEntity(command);

        if (_notify.HasNotifications())
        {
            return null;
        }
        
        var validationResult = entity.Validate();

        if (!validationResult.IsValid)
        {
            _notify.NewNotification(validationResult.Erros);
            return null;
        }

        _taskListRepository.Add(entity);

        if (!_taskListRepository.SaveChanges())
        {
            _notify.NewNotification(ErrorMessage.SAVE_DATA.Code, ErrorMessage.SAVE_DATA.Message);
            return null;
        }

        return _mapper.Map<CreateTaskListEvent>(entity);
    }
    
    private TaskList CreateTaskListEntity(CreateTaskListCommand taskListDto)
    {
        var taskItens = new List<TaskItem>();

        if (taskListDto.TaskItems != null && taskListDto.TaskItems.Any())
        {
            foreach (var taskItemDto in taskListDto.TaskItems)
            {
                var taskItem = new TaskItem(taskItemDto.Title, taskItemDto.Description, taskItemDto.DueDate);
                
                var validationResult = taskItem.Validate();

                if (!validationResult.IsValid)
                {
                    _notify.NewNotification(validationResult.Erros);
                    continue;
                }
                
                taskItens.Add(taskItem);
            }
        }

        var entity = new TaskList(taskListDto.Title, taskItens);

        return entity;
    }
}