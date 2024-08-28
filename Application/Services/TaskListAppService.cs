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
        var entity = CreateTaskListEntity(taskListDto);

        _taskListRepository.Add(entity);

        if (!_taskListRepository.SaveChanges())
        {
            throw new Exception("Erro ao Cadastrar o TaskList");
        }

        return _mapper.Map<TaskListViewModel>(entity);
    }

    public TaskListViewModel UpdateTaskList(UpdateTaskListDto taskListDto)
    {
        var entity = _taskListRepository.Query<TaskList>(x => x.Id == taskListDto.Id).FirstOrDefault();

        if (entity == null)
            throw new ApplicationException("not found");

        entity.Update(taskListDto.Title);

        _taskListRepository.Update(entity);

        if (!_taskListRepository.SaveChanges())
        {
            throw new ApplicationException("Error updating task list");
        }

        return _mapper.Map<TaskListViewModel>(entity);
    }

    private static TaskList CreateTaskListEntity(CreateTaskListDto taskListDto)
    {
        var taskItens = new List<TaskItem>();

        if (taskListDto.TaskItems != null && taskListDto.TaskItems.Any())
        {
            taskItens.AddRange(taskListDto.TaskItems.Select(taskItemDto =>
                new TaskItem(taskItemDto.Title, taskItemDto.Description, taskItemDto.DueDate)));
        }

        var entity = new TaskList(taskListDto.Title, taskItens);

        return entity;
    }
}