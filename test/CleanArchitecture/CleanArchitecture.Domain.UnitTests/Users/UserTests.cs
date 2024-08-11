using CleanArchitecture.Domain.Users;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Domain.UnitTests.Users;

public class UserTests
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
}