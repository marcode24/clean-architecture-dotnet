namespace CleanArchitecture.Domain.Abstractions;

public abstract class Entity
{
  private readonly List<IDomainEvent> _domainEvents = [];
  protected Entity(Guid id) => Id = id;
  public Guid Id { get; init; } //init para que nunca se pueda modificar el id
  public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();
  public void ClearDomainEvents() => _domainEvents.Clear();
  protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}