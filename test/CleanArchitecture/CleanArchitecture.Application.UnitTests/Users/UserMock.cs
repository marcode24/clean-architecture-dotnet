using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.UnitTests.Users;

internal static class UserMock
{
  public static readonly Nombre Nombre = new("Nombre");
  public static readonly Apellido Apellido = new("Apellido");
  public static readonly Email Email = new("test@gmail.com");
  public static readonly PasswordHash Password = new("Password123$");

  public static User Create() => User.Create(
    Nombre,
    Apellido,
    Email,
    Password
  );
}