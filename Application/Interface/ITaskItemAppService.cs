using Application.Dto.TaskItem;
using Application.ViewModels;

namespace Application.Interface;

public interface ITaskItemAppService
{
    TaskItemViewModel CreateTaskItem(CreatePartialTaskItemDto partialTaskItem);
    TaskItemViewModel UpdateTaskItem(UpdateTaskItemDto taskItem);
}