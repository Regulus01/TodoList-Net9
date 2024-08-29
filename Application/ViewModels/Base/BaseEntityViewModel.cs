using System.Text.Json.Serialization;

namespace Application.ViewModels.Base;

public abstract class BaseEntityViewModel
{
    
    [JsonPropertyOrder(-1)]
    public Guid Id { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset UpdateDate { get; set; }
}