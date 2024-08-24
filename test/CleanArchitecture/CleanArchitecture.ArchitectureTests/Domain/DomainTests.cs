using System.Reflection;
using CleanArchitecture.ArchitectureTests.Infrastructure;
using CleanArchitecture.Domain.Abstractions;
using FluentAssertions;
using NetArchTest.Rules;
using Xunit;

namespace CleanArchitecture.ArchitectureTests.Domain;

public class DomainTests : BaseTest
{
  [Fact]
  public void Entities_ShouldHave_PrivateConstructorNoParameteres()
  {
    IEnumerable<Type> entityTypes = Types.InAssembly(DomainAssembly)
      .That()
      .Inherit(typeof(Entity<>))
      .GetTypes();

    var errorEntities = new List<Type>();

    foreach (var entityType in entityTypes)
    {
      ConstructorInfo[] constructors = entityType.GetConstructors(
        BindingFlags.Instance | BindingFlags.NonPublic
      );

      if (!constructors.Any(c => c.IsPrivate && c.GetParameters().Length == 0))
        errorEntities.Add(entityType);
    }

    errorEntities.Should().BeEmpty();
  }
}