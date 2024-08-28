using Application.Dto.TaskItem;
using Application.ViewModels;

namespace Application.Interface;

public interface ITaskItemAppService
{
    TaskItemViewModel CreateTaskItem(CreateTaskItemDto partialTaskItem);
    TaskItemViewModel UpdateTaskItem(UpdateTaskItemDto taskItem);
}