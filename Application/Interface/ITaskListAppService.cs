using Application.Dto.TaskList;
using Application.ViewModels;
using Application.ViewModels.TaskList;

namespace Application.Interface;

public interface ITaskListAppService
{
    TaskListViewModel Create(CreateTaskListDto taskListDto);
    TaskListViewModel Get(Guid? id);
    TaskListViewModel Update(UpdateTaskListDto taskListDto);
    void Delete(Guid? id);
}