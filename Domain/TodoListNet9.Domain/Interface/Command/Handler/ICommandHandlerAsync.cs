using Domain.Interface.Command.Marking;

namespace Infra.CrossCutting.Command.Interface.Handler;

public interface ICommandHandlerAsync<in TCommand> where TCommand : ICommand
{
    public Task HandleAsync(TCommand command);
}