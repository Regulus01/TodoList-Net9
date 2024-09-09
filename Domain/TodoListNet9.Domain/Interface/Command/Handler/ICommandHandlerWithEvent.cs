#region

using Domain.Interface.Command.Marking;

#endregion

namespace Infra.CrossCutting.Command.Interface.Handler;

public interface ICommandHandlerAsyncWithEvent<in TCommand, out TEvent> where TCommand : ICommand 
                                                                        where TEvent : IEvent?
{
    public TEvent HandleAsync(TCommand command);
}