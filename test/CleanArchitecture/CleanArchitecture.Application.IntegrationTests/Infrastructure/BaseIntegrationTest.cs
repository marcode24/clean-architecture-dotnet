using CleanArchitecture.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CleanArchitecture.Application.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
  private readonly IServiceScope _serviceScope;
  protected readonly ISender Sender;
  protected readonly ApplicationDbContext dbContext;

  protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
  {
    _serviceScope = factory.Services.CreateScope();
    Sender = _serviceScope.ServiceProvider.GetRequiredService<ISender>();
    dbContext = _serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
  }
}