using System.Text.Json.Serialization;

namespace Application.ViewModels.Base;

public abstract class BaseEntityViewModel
{
    /// <summary>
    /// Id of the entity
    /// </summary>
    [JsonPropertyOrder(-1)]
    public Guid Id { get; set; }
    /// <summary>
    /// Date it was created
    /// </summary>
    public DateTimeOffset CreationDate { get; set; }
    /// <summary>
    /// Date it was updated
    /// </summary>
    public DateTimeOffset UpdateDate { get; set; }
}