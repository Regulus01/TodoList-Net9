using Domain.Interface.Notification;
using NotificationModel = Domain.Notification.Model.Notification;

namespace Infra.CrossCutting.Notification;

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

    public void NewNotification(IDictionary<string, string> erros)
    {
        foreach (var erro in erros)
        {
            _notifications.Add(new NotificationModel(erro.Key, erro.Value));   
        }
    }
}