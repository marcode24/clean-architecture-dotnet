using Asp.Versioning;
using CleanArchitecture.Application.Abstractions.Authentication;
using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Application.Paginations;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using CleanArchitecture.Infrastructure.Authentication;
using CleanArchitecture.Infrastructure.Clock;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Email;
using CleanArchitecture.Infrastructure.Outbox;
using CleanArchitecture.Infrastructure.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {

    services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));
    services.AddQuartz();
    services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);
    services.ConfigureOptions<ProcessOutboxMessageSetup>();

    services.AddApiVersioning(opt =>
    {
      opt.DefaultApiVersion = new ApiVersion(1);
      opt.ReportApiVersions = true;
      opt.ApiVersionReader = new UrlSegmentApiVersionReader();
    }).AddMvc()
      .AddApiExplorer(opt =>
      {
        opt.GroupNameFormat = "'v'V";
        opt.SubstituteApiVersionInUrl = true;
      });

    services.AddTransient<IDateTimeProvider, DateTimeProvider>();
    services.AddTransient<IEmailService, EmailService>();

    var connectionString = configuration.GetConnectionString("DataBaseConnection")
      ?? throw new ArgumentNullException(nameof(configuration));

    services.AddDbContext<ApplicationDbContext>(options =>
    {
      options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
    });

    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IPaginationRepository, UserRepository>();
    services.AddScoped<IVehiculosRepository, VehicleRepository>();
    services.AddScoped<IAlquilerRepository, AlquilerRepository>();

    services.AddScoped<IUnitOfWork, IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

    services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

    SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

    services.AddHttpContextAccessor();
    services.AddScoped<IUserContext, UserContext>();

    return services;
  }
}