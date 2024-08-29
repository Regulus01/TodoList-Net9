using Application.Dto.TaskList;
using Application.Interface;
using Application.ViewModels;
using Application.ViewModels.TaskList;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;

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

    public TaskListViewModel Create(CreateTaskListDto taskListDto)
    {
        var entity = CreateTaskListEntity(taskListDto);

        _taskListRepository.Add(entity);

        if (!_taskListRepository.SaveChanges())
        {
            throw new Exception("Erro ao Cadastrar o TaskList");
        }

        return _mapper.Map<TaskListViewModel>(entity);
    }

    public TaskListViewModel Get(Guid? id)
    {
        var taskList = _taskListRepository.Query<TaskList>(x => x.Id == id,
            y => y.Include(i => i.TaskItems)!).FirstOrDefault();
        
        if (taskList == null)
            throw new ApplicationException("not found");

        return _mapper.Map<TaskListViewModel>(taskList);
    }

    public TaskListViewModel Update(UpdateTaskListDto taskListDto)
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

    public void Delete(Guid? id)
    {
        var taskList = _taskListRepository.Query<TaskList>(x => x.Id == id,
            y => y.Include(i => i.TaskItems)!).FirstOrDefault();

        ValidateDeleteTaskListDto(taskList);

        _taskListRepository.Delete(taskList);
        
        if (!_taskListRepository.SaveChanges())
        {
            throw new Exception("Erro ao deletar o TaskList");
        }
    }

    private void ValidateDeleteTaskListDto(TaskList taskList)
    {
        if (taskList == null)
            throw new ApplicationException("not found");

        if (taskList.TaskItems != null)
            throw new ApplicationException("task list have items");
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