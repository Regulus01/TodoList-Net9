using Application.Dto.TaskItem;
using Application.ViewModels.TaskItem;

namespace Application.Interface;

public interface ITaskItemAppService
{
    TaskItemViewModel? CreateTaskItem(CreateTaskItemDto dto);
    TaskItemViewModel? UpdateTaskItem(UpdateTaskItemDto taskItem);
}