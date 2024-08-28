using Application.Dto.TaskItem;
using Application.Interface;
using Application.ViewModels;
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
    
    public TaskItemViewModel CreateTaskItem(CreatePartialTaskItemDto partialTaskItem)
    {
        var entityMap = _mapper.Map<TaskItem>(partialTaskItem);
        
        _taskItemRepository.Create(entityMap);
        _taskItemRepository.SaveChanges();
        
        return _mapper.Map<TaskItemViewModel>(entityMap);
    }

    public TaskItemViewModel UpdateTaskItem(UpdateTaskItemDto taskItem)
    {
        var entity = _taskItemRepository.Query<TaskItem>(x => x.Id == taskItem.Id);

        if (entity == null)
            throw new ApplicationException("Not found");
        
        var item = _mapper.Map<TaskItem>(taskItem);
        
        _taskItemRepository.Update(item);
        
        return _mapper.Map<TaskItemViewModel>(item);
    }
}