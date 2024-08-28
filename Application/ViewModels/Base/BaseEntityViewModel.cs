namespace Application.ViewModels.Base;

public abstract class BaseEntityViewModel
{
    public Guid Id { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset UpdateDate { get; set; }
}