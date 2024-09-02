using Domain.Interface.Base;
using Infra.CrossCutting.Notification.Interface;
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

    public void SetupSaveChanges<T>() where T : class, IBaseRepository
    {
        Mocker.GetMock<T>()
            .Setup(x => x.SaveChanges(It.IsAny<CancellationToken>()))
            .Returns(true);
    }
}