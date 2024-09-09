using Domain.Interface.Base;
using Domain.Interface.Command;
using Domain.Interface.Command.Marking;
using Domain.Interface.Notification;
using Moq;
using Moq.AutoMock;

namespace TodoListNet9.Application.Test.Base;

public abstract class BaseFixture
{
    public AutoMocker Mocker { get; protected set; }

    public void SetupHasNotification(bool hasNotification)
    {
        Mocker.GetMock<INotify>()
              .Setup(x => x.HasNotifications())
              .Returns(hasNotification);
    }

    public void SetupSaveChanges<T>(bool success = true) where T : class, IBaseRepository
    {
        Mocker.GetMock<T>()
            .Setup(x => x.SaveChanges())
            .Returns(success);
    }
    
    public void NeverNotifications()
    {
        Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(It.IsAny<IDictionary<string, string>>()),
                Times.Never);

        Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);
    }
    
    public void SetupCommandWithEvent<TCommand, TEvent>(TEvent eventResponse) where TCommand : ICommand where TEvent : IEvent?
    {
        Mocker.GetMock<ICommandInvoker>()
              .Setup(x => x.Execute<TCommand, TEvent>(It.IsAny<TCommand>()))
              .Returns(eventResponse);
    }
}