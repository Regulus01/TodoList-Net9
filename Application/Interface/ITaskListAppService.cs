using Application.Dto;
using Application.Dto.TaskList;
using Application.ViewModels;

namespace Application.Interface;

public interface ITaskListAppService
{
    TaskListViewModel CreateTaskList(CreateTaskListDto taskItem);
    TaskItemViewModel UpdateTaskTitle(UpdateTaskListTitleDto taskItem);
}