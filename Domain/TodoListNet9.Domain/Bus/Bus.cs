using Domain.Interface.Command;
using Domain.Interface.Notification;

namespace Domain.Bus;

public class Bus
{
    public ICommandInvoker Command { get; private set; }
    public INotify Notify { get; private set; }

    public Bus(ICommandInvoker command, INotify notify)
    {
        Command = command;
        Notify = notify;
    }
}