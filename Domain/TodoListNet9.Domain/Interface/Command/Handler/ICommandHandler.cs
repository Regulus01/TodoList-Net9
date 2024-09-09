using Domain.Interface.Command.Marking;

namespace Infra.CrossCutting.Command.Interface.Handler;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    public void Handle(TCommand command);
}