using CleanArchitecture.Application.Users.RegisterUser;

namespace CleanArchitecture.Api.FunctionalTests.Users;

internal static class UserData
{
  public static RegisterUserRequest registerUserRequestTest = new(
    "tests@gmail.com",
    "Test1234",
    "Test1234",
    "TestPassword1234$"
  );
}