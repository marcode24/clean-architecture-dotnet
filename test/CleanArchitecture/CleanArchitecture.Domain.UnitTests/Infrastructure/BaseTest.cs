using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.UnitTests.Infrastructure;

public abstract class BaseTest
{
  public static T AssertDomainWasPublished<T>(IEntity entity) where T : IDomainEvent
  {
    var domainEvent = entity.GetDomainEvents().OfType<T>().SingleOrDefault();
    if (domainEvent is null)
      throw new Exception($"Domain event of type {typeof(T).Name} was not published.");

    return domainEvent!;
  }
}