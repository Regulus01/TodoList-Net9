using System.Text.Json.Serialization;

namespace Infra.CrossCutting.Notification.Model;

public class Notification
{
    [JsonPropertyOrder(-2)]
    public string Key { get; }
    
    [JsonPropertyOrder(-1)]
    public string Value { get; }

    public Notification(string key, string value)
    {
        Key = key;
        Value = value;
    }
}