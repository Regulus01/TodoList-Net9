using Domain.Interface.Command.Marking;

namespace Domain.Interface.Command.Handler;

public interface ICommandHandlerWithEvent<in TCommand, out TEvent> where TCommand : ICommand 
                                                                   where TEvent : IEvent?
{
    public TEvent Handle(TCommand command);
}