using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Domain.UnitTests.Users;

internal class UserMock
{
  public static readonly Nombre nombre = new("Test");
  public static readonly Apellido apellido = new("User");
  public static readonly Email email = new("test@gmail.com");
  public static readonly PasswordHash password = new("MyPassword123$");
}