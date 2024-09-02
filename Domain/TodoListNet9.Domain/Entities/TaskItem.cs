using Domain.Entities.Base;
using Domain.Resourcers;

namespace Domain.Entities;

public class TaskItem : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTimeOffset? DueDate { get; private set; }
    public bool IsCompleted { get; private set; }
    public Guid TaskListId { get; private set; }
    public TaskList TaskList { get; private set; }

    public TaskItem(string title, string description, DateTimeOffset? dueDate)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        IsCompleted = false;
    }
    
    public TaskItem(string title, string description, DateTimeOffset? dueDate, Guid taskListId)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        IsCompleted = false;
        TaskListId = taskListId;
    }
    
    public void Update(string title, string description, DateTimeOffset? dueDate, bool isCompleted)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        IsCompleted = isCompleted;
    }

    public override (bool IsValid, IDictionary<string, string> Erros) Validate()
    {
        var erros = new Dictionary<string, string>();

        if (string.IsNullOrWhiteSpace(Title))
        {
            erros.Add(ErrorMessage.TITLE_REQUIRED.Code, ErrorMessage.TITLE_REQUIRED.Message);
        }

        if (DueDate.HasValue && DueDate.Value.Date < DateTimeOffset.UtcNow.Date)
        {
            erros.Add(ErrorMessage.DUE_DATE_IN_PAST.Code, ErrorMessage.DUE_DATE_IN_PAST.Message);
        }

        return (erros.Count == 0, erros);
    }
}