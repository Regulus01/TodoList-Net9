using Domain.Entities.Base;

namespace Domain.Entities;

public class TaskList : BaseEntity
{
    public string Title { get; private set; }
    public IEnumerable<TaskItem>? TaskItems { get; private set; }

    public TaskList()
    {
        
    }
    public TaskList(string title)
    {
        Title = title;
    }
    
    public TaskList(string title, IEnumerable<TaskItem> taskItems)
    {
        Title = title;
        TaskItems = taskItems;
    }
}