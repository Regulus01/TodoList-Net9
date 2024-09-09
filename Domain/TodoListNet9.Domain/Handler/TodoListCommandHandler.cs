using AutoMapper;
using Domain.Commands;
using Domain.Commands.Events;
using Domain.Entities;
using Domain.Interface;
using Domain.Interface.Command.Handler;
using Domain.Interface.Repository;
using Domain.Resourcers;
using Infra.CrossCutting.Command.Interface.Handler;
using DomainBus = Domain.Bus.Bus;

namespace Domain.Handler;

public class TodoListCommandHandler : ICommandHandlerWithEvent<CreateTaskListCommand, CreateTaskListEvent?>
{
    private readonly ITaskListRepository _taskListRepository;
    private readonly IMapper _mapper;
    private readonly DomainBus _bus;


    public TodoListCommandHandler(ITaskListRepository taskListRepository, IMapper mapper, DomainBus bus)
    {
        _taskListRepository = taskListRepository;
        _mapper = mapper;
        _bus = bus;
    }
    
    public CreateTaskListEvent? Handle(CreateTaskListCommand command)
    {
        var entity = CreateTaskListEntity(command);
        
        if (_bus.Notify.HasNotifications())
        {
            return null;
        }
        
        var validationResult = entity.Validate();

        if (!validationResult.IsValid)
        {
            _bus.Notify.NewNotification(validationResult.Erros);
            return null;
        }

        _taskListRepository.Add(entity);

        if (!_taskListRepository.SaveChanges())
        {
            _bus.Notify.NewNotification(ErrorMessage.SAVE_DATA.Code, ErrorMessage.SAVE_DATA.Message);
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
                    _bus.Notify.NewNotification(validationResult.Erros);
                    continue;
                }
                
                taskItens.Add(taskItem);
            }
        }

        var entity = new TaskList(taskListDto.Title, taskItens);

        return entity;
    }
}