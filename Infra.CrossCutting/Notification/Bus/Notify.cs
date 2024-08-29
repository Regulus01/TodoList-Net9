using Infra.CrossCutting.Notification.Interface;
using NotificationModel = Infra.CrossCutting.Notification.Model.Notification;

namespace Infra.CrossCutting.Notification.Bus;

public class Notify : INotify
{
    private List<NotificationModel> _notifications = [];
    
    public IEnumerable<NotificationModel> GetNotifications()
    {
        return _notifications.Where(not => not.GetType() == typeof(NotificationModel)).ToList();
    }

    public bool HasNotifications()
    {
        return GetNotifications().Any();
    }

    public void NewNotification(string key, string message)
    {
        _notifications.Add(new NotificationModel(key, message));
    }
}