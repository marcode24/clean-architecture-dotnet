using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Infrastructure.Clock;
using CleanArchitecture.Infrastructure.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    services.AddTransient<IDateTimeProvider, DateTimeProvider>();
    services.AddTransient<IEmailService, EmailService>();
    return services;
  }
}