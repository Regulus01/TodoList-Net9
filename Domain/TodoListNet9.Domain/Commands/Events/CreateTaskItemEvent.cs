namespace Domain.Commands.Events;

public class CreateTaskItemEvent : BaseEvent
{
    /// <summary>
    /// Title of the task item
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Description of the task item
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Date the task item has expired
    /// </summary>
    public DateTimeOffset? DueDate { get; set; }

    /// <summary>
    /// Status of the task item
    /// </summary>
    /// <list type="bullet">
    /// <item>
    /// <description>True if the task list is completed</description>
    /// </item>
    /// <item>
    /// <description>False if the task list is not completed</description>
    /// </item>
    /// </list>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Id of the task list
    /// </summary>
    public Guid TaskListId { get; set; }
}