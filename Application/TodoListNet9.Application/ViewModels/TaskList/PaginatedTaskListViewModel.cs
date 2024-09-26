using Application.ViewModels.Base;

namespace Application.ViewModels.TaskList;

public class PaginatedTaskListViewModel : BaseEntityViewModel
{
    /// <summary>
    /// Title of the task 
    /// </summary>
    public string Title { get;  set; }
}