using Application.Dto.TaskItem;
using Application.ViewModels.TaskItem;

namespace Application.Interface;

public interface ITaskItemAppService
{
    /// <summary>
    /// Create one task item
    /// </summary>
    /// <param name="dto">data for creation</param>
    /// <returns>Task list has been created</returns>
    TaskItemViewModel? CreateTaskItem(CreateTaskItemDto dto);
    
    /// <summary>
    /// Update one task item
    /// </summary>
    /// <param name="taskItem">data for update</param>
    /// <returns>Task list has been updated</returns>
    TaskItemViewModel? UpdateTaskItem(UpdateTaskItemDto taskItem);
}