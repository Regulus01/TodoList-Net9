using Domain.Interface.Command.Marking;
using Infra.CrossCutting.Command.Interface;
using Infra.CrossCutting.Command.Interface.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting.Command;

public class CommandInvoker : ICommandInvoker
{
    private readonly IServiceProvider _serviceProvider;

    public CommandInvoker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Execute<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = _serviceProvider.GetService<ICommandHandler<TCommand>>();

        if (handler == null)
            throw new Exception($"No handler registered for type {typeof(TCommand).Name}");
        
        handler.Handle(command);
    }

    public TEvent Execute<TCommand, TEvent>(TCommand command) where TCommand : ICommand where TEvent : IEvent?
    {
        var handler = _serviceProvider.GetService<ICommandHandlerWithEvent<TCommand, TEvent>>();

        if (handler == null)
            throw new Exception($"No handler registered for type {typeof(TCommand).Name} - {typeof(TEvent).Name}");
        
        return handler.Handle(command);
    }

    public async Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = _serviceProvider.GetService<ICommandHandlerAsync<TCommand>>();
        
        if (handler == null)
            throw new Exception($"No handler registered for type {typeof(TCommand).Name}");
            
        await handler.HandleAsync(command);
    }

    public TEvent ExecuteAsync<TCommand, TEvent>(TCommand command) where TCommand : ICommand where TEvent : IEvent?
    {
        var handler = _serviceProvider.GetService<ICommandHandlerAsyncWithEvent<TCommand, TEvent>>();

        if (handler == null)
            throw new Exception($"No handler registered for type {typeof(TCommand).Name} - {typeof(TEvent).Name}");
        
        return handler.HandleAsync(command);
    }
}