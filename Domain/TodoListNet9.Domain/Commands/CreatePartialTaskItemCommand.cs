using Domain.Interface.Command.Marking;

namespace Domain.Commands;

public class CreatePartialTaskItemCommand : ICommand
{
    public string Title { get; set; }
    public string Description { get;set; }
    public DateTimeOffset? DueDate { get; set; }
}