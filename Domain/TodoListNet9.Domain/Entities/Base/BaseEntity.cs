namespace Domain.Entities.Base;

public abstract class BaseEntity
{
    public Guid Id { get; private set; }
    public DateTimeOffset CreationDate { get; private set; }
    public DateTimeOffset UpdateDate { get; private set; }

    public void ChangeCreationDate(DateTimeOffset date)
    {
        CreationDate = date;
    }

    public void ChangeUpdateDate(DateTimeOffset date)
    {
        UpdateDate = date;
    }

    public abstract (bool IsValid, IDictionary<string, string> Erros) Validate();
}