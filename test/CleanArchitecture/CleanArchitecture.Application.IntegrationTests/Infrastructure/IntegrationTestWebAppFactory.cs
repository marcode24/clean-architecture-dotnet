
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CleanArchitecture.Application.IntegrationTests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
  public Task InitializeAsync()
  {
    throw new NotImplementedException();
  }

  Task IAsyncLifetime.DisposeAsync()
  {
    throw new NotImplementedException();
  }
}