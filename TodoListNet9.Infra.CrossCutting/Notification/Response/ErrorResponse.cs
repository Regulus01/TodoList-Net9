namespace Infra.CrossCutting.Notification.Response;

using Notification = Model.Notification;

public class ErrorResponse
{
    public bool Sucess { get; private set; }
    public DateTimeOffset Time { get; private set; }
    public IEnumerable<Notification> Error { get; private set; }

    public ErrorResponse(IEnumerable<Notification> error)
    {
        Sucess = false;
        Time = DateTimeOffset.UtcNow;
        Error = error;
    }
}