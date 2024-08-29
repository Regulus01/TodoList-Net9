using Application.Dto.TaskItem;
using Application.Interface;
using Application.ViewModels;
using Application.ViewModels.TaskItem;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;

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
            throw new ApplicationException("TaskItem not found");
        
        var entity = new TaskItem(dto.Title, dto.Description, dto.DueDate, dto.TaskListId);

        _taskItemRepository.Add(entity);

        if (!_taskItemRepository.SaveChanges())
            throw new Exception("Erro ao cadastrar o TaskItem");

        return _mapper.Map<TaskItemViewModel>(entity);
    }

    public TaskItemViewModel UpdateTaskItem(UpdateTaskItemDto taskItem)
    {
        var entity = _taskItemRepository.Query<TaskItem>(x => x.Id == taskItem.Id).FirstOrDefault();

        if (entity == null)
            throw new ApplicationException("Not found");

        entity.Update(taskItem.Title, taskItem.Description, taskItem.DueDate, taskItem.IsCompleted);

        _taskItemRepository.Update(entity);
        
        if (!_taskItemRepository.SaveChanges())
            throw new Exception("Erro ao cadastrar o TaskItem");

        return _mapper.Map<TaskItemViewModel>(entity);
    }
}