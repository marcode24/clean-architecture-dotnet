using CleanArchitecture.Application.Abstractions.Behaviors;
using CleanArchitecture.Domain.Alquileres;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddMediatR(configuration =>
    {
      configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
      configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
    });

    services.AddTransient<PrecioService>();

    return services;
  }
}