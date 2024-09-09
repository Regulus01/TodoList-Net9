using Domain.Interface.Command.Marking;

namespace Infra.CrossCutting.Command.Interface.Handler;

public interface ICommandHandlerWithEvent<in TCommand, out TEvent> where TCommand : ICommand 
                                                                   where TEvent : IEvent?
{
    public TEvent Handle(TCommand command);
}