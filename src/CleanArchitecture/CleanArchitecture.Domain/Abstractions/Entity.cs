namespace CleanArchitecture.Domain.Abstractions;

public abstract class Entity
{
  protected Entity(Guid id) => Id = id;
  public Guid Id { get; init; } //init para que nunca se pueda modificar el id
}