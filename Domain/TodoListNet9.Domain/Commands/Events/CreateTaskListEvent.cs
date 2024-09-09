namespace Domain.Commands.Events;

public class CreateTaskListEvent : BaseEvent
{
    /// <summary>
    /// Title of the task 
    /// </summary>
    public string Title { get;  set; }
    public IEnumerable<CreateTaskItemEvent>? TaskItems { get;  set; }
}