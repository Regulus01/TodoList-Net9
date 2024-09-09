using Domain.Interface.Command.Marking;

namespace Domain.Commands;

public class CreateTaskListCommand : ICommand
{
    public string Title { get; set; }
    
    public IEnumerable<CreatePartialTaskItemCommand>? TaskItems { get; set; }
}