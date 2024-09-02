using Domain.Entities.Base;
using Domain.Resourcers;

namespace Domain.Entities;

public class TaskList : BaseEntity
{
    public string Title { get; private set; }
    public IEnumerable<TaskItem>? TaskItems { get; private set; }
    
    public TaskList(string title)
    {
        Title = title;
    }
    
    public TaskList(string title, IEnumerable<TaskItem>? taskItems)
    {
        Title = title;
        TaskItems = taskItems;
    }
    
    public void Update(string title)
    {
        Title = title;
    }
    
    public override (bool IsValid, IDictionary<string, string> Erros) Validate()
    {
        var erros = new Dictionary<string, string>();

        if (string.IsNullOrWhiteSpace(Title))
        {
            erros.Add(ErrorMessage.TITLE_REQUIRED.Code, ErrorMessage.TITLE_REQUIRED.Message);
        }

        return (erros.Count == 0, erros);
    }
}