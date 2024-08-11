using CleanArchitecture.Domain.UnitTests.Infrastructure;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Users.Events;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Domain.UnitTests.Users;

public class UserTests : BaseTest
{
  [Fact]
  public void Create_Should_SetPropertyValues()
  {
    var user = User.Create(UserMock.nombre, UserMock.apellido, UserMock.email, UserMock.password);

    user.Nombre.Should().Be(UserMock.nombre);
    user.Apellido.Should().Be(UserMock.apellido);
    user.Email.Should().Be(UserMock.email);
    user.PasswordHash.Should().Be(UserMock.password);
  }

  [Fact]
  public void Create_Should_RaiseUserCreatedDomainEvent()
  {
    var user = User.Create(UserMock.nombre, UserMock.apellido, UserMock.email, UserMock.password);

    // var domainEvent = user.GetDomainEvents().OfType<UserCreatedDomainEvent>().SingleOrDefault();
    var domainEvent = AssertDomainWasPublished<UserCreatedDomainEvent>(user);

    domainEvent!.UserId.Should().Be(user.Id);
  }
}