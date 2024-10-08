﻿using Domain.Interface.Command.Marking;

namespace Domain.Interface.Command;

public interface ICommandInvoker
{
    public void Execute<TCommand>(TCommand command) where TCommand : ICommand;
    public TEvent Execute<TCommand, TEvent>(TCommand command) where TCommand : ICommand where TEvent : IEvent?;
    public Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;
    public TEvent ExecuteAsync<TCommand, TEvent>(TCommand command) where TCommand : ICommand where TEvent : IEvent?;
}