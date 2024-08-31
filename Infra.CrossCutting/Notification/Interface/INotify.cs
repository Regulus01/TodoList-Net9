namespace Infra.CrossCutting.Notification.Interface;
using Notification = Model.Notification;

public interface INotify
{
    bool HasNotifications();
    IEnumerable<Notification> GetNotifications();
    void NewNotification(string key, string message);
    void NewNotification(IDictionary<string, string> erros);
}