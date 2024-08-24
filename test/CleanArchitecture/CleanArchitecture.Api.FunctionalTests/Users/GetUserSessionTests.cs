using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CleanArchitecture.Api.FunctionalTests.Infrastructure;
using CleanArchitecture.Application.Users.GetUserSession;
using CleanArchitecture.Application.Users.LoginUser;
using CleanArchitecture.Application.Users.RegisterUser;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Xunit;

namespace CleanArchitecture.Api.FunctionalTests.Users;

public class GetUserSessionTests : BaseFunctionalTest
{
  public GetUserSessionTests(FunctionalTestWebAppFactory factory) : base(factory) { }

  [Fact]
  public async Task Get_ShouldReturnUnauthorized_WhenTokenIsNotProvided()
  {
    var response = await httpClient.GetAsync("api/v1/users/me");

    response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
  }

  [Fact]
  public async Task Get_ShouldReturnUser_WhenTokenExists()
  {
    var token = await GetToken();
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
      JwtBearerDefaults.AuthenticationScheme,
      token
    );

    var user = await httpClient.GetFromJsonAsync<UserResponse>("api/v1/users/me");

    user.Should().NotBeNull();
  }

  [Fact]
  public async Task Login_ShouldReturnOK_WhenUserExists()
  {
    var request = new LoginUserRequest(
      UserData.registerUserRequestTest.Email,
      UserData.registerUserRequestTest.Password
    );

    var response = await httpClient.PostAsJsonAsync("api/v1/users/login", request);

    response.StatusCode.Should().Be(HttpStatusCode.OK);
  }

  [Fact]
  public async Task Register_ShouldReturnOK_WhenRequestIsValid()
  {
    var request = new RegisterUserRequest(
      "test1@gmail.com",
      "Test1234",
      "Test1234",
      "TestPassword21#"
    );

    var response = await httpClient.PostAsJsonAsync("api/v1/users/register", request);

    response.StatusCode.Should().Be(HttpStatusCode.OK);
  }
}