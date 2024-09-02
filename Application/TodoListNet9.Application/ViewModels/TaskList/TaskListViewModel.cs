using Application.ViewModels.Base;
using Application.ViewModels.TaskItem;

namespace Application.ViewModels.TaskList;

public class TaskListViewModel : BaseEntityViewModel
{
    /// <summary>
    /// Title of the task 
    /// </summary>
    public string Title { get;  set; }
    public IEnumerable<TaskItemViewModel>? TaskItems { get;  set; }
}