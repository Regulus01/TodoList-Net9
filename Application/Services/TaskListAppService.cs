using Application.Dto.TaskList;
using Application.Interface;
using Application.ViewModels.TaskList;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Domain.Resourcers;
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
            throw new Exception(ErrorMessage.ERROR_TO_SAVE);
        }

        return _mapper.Map<TaskListViewModel>(entity);
    }

    public TaskListViewModel Get(Guid? id)
    {
        var taskList = _taskListRepository.Query<TaskList>(x => x.Id == id,
            y => y.Include(i => i.TaskItems)!).FirstOrDefault();

        if (taskList == null)
            throw new ApplicationException(ErrorMessage.NOT_FOUND);

        return _mapper.Map<TaskListViewModel>(taskList);
    }

    public TaskListViewModel Update(UpdateTaskListDto taskListDto)
    {
        var entity = _taskListRepository.Query<TaskList>(x => x.Id == taskListDto.Id).FirstOrDefault();

        if (entity == null)
            throw new ApplicationException(ErrorMessage.NOT_FOUND);

        entity.Update(taskListDto.Title);

        _taskListRepository.Update(entity);

        if (!_taskListRepository.SaveChanges())
        {
            throw new ApplicationException(ErrorMessage.ERROR_TO_UPDATE);
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
            throw new Exception(ErrorMessage.ERROR_TO_DELETE);
        }
    }

    private void ValidateDeleteTaskListDto(TaskList taskList)
    {
        if (taskList == null)
            throw new ApplicationException(ErrorMessage.NOT_FOUND);

        if (taskList.TaskItems != null)
            throw new ApplicationException(ErrorMessage.ERROR_LINKED_TASKITEM);
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