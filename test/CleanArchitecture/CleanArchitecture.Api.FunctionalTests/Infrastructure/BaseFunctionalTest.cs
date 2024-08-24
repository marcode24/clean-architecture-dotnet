using System.Net.Http.Json;
using CleanArchitecture.Api.FunctionalTests.Users;
using CleanArchitecture.Application.Users.LoginUser;
using Xunit;

namespace CleanArchitecture.Api.FunctionalTests.Infrastructure;

public abstract class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
{
  protected readonly HttpClient httpClient;
  protected BaseFunctionalTest(FunctionalTestWebAppFactory factory)
  {
    httpClient = factory.CreateClient();
  }

  protected async Task<string> GetToken()
  {
    HttpResponseMessage response = await httpClient.PostAsJsonAsync(
      "api/v1/users/login",
      new LoginUserRequest(
        UserData.registerUserRequestTest.Email,
        UserData.registerUserRequestTest.Password
      )
    );

    return await response.Content.ReadAsStringAsync();
  }
}