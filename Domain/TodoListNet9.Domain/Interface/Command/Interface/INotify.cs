namespace Domain.Interface.Command.Interface;
using NotificationDomain = Domain.Notification.Model.Notification;

public interface INotify
{
    bool HasNotifications();
    IEnumerable<NotificationDomain> GetNotifications();
    void NewNotification(string key, string message);
    void NewNotification(IDictionary<string, string> erros);
}