using Application.ViewModels.Base;

namespace Application.ViewModels.TaskItem;

public class TaskItemViewModel : BaseEntityViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public Guid TaskListId { get; set; }
}