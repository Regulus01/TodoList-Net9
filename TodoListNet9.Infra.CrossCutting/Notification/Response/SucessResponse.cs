namespace Infra.CrossCutting.Notification.Response;

public class SucessResponse
{
    public bool Sucess { get; private set; }
    public DateTimeOffset Time { get; private set; }
    public object? Data { get; private set; }

    public SucessResponse(object? data = null)
    {
        Sucess = true;
        Time = DateTimeOffset.UtcNow;
        Data = data;
    }
}