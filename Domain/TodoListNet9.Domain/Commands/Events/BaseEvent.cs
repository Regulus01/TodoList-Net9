using Domain.Interface.Command.Marking;

namespace Domain.Commands.Events;

public class BaseEvent : IEvent
{
    public Guid Id { get;  set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset UpdateDate { get; set; }
}