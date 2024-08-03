using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Users;

public static class UsersErrors
{
  public static readonly Error NotFound = new(
    "user_not_found",
    "The user was not found."
  );
  public static readonly Error InvalidCredentials = new(
    "invalid_credentials",
    "The credentials are invalid."
  );
}