using Domain.Entities.Base;

namespace Domain.Entities;

public class TaskItem : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTimeOffset? DueDate { get; private set; }
    public bool IsCompleted { get; private set; }
    public Guid TaskListId { get; private set; }
    public TaskList TaskList { get; private set; }

    public TaskItem()
    {
        
    }
    public TaskItem(string title, string description, DateTimeOffset? dueDate, Guid taskListId)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        IsCompleted = false;
        TaskListId = taskListId;
    }
}