using Application.ViewModels.Base;

namespace Application.ViewModels;

public class TaskListViewModel : BaseEntityViewModel
{
    public string Title { get;  set; }
    public IEnumerable<TaskItemViewModel>? TaskItems { get;  set; }
}