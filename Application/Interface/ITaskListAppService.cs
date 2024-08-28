using Application.Dto.TaskList;
using Application.ViewModels;

namespace Application.Interface;

public interface ITaskListAppService
{
    TaskListViewModel CreateTaskList(CreateTaskListDto taskListDto);
    TaskListViewModel UpdateTaskList(UpdateTaskListDto taskListDto);
}