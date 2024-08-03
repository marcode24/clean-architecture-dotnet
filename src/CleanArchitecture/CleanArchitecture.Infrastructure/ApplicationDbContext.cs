using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
  private readonly IPublisher _publisher;
  public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
  {
    _publisher = publisher;
  }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    base.OnModelCreating(modelBuilder);
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    try
    {
      var result = await base.SaveChangesAsync(cancellationToken);
      await PublishDomainEventsAsync();

      return result;

    }
    catch (DbUpdateConcurrencyException ex)
    {
      throw new ConcurrencyException("A concurrency error occurred while saving the data", ex);
    }
  }

  private async Task PublishDomainEventsAsync()
  {
    var domainEvents = ChangeTracker
      .Entries<Entity>()
      .Select(e => e.Entity)
      .SelectMany(e =>
      {
        var domainEvents = e.GetDomainEvents();
        e.ClearDomainEvents();
        return domainEvents;
      }).ToList();

    foreach (var domainEvent in domainEvents)
    {
      await _publisher.Publish(domainEvent);
    }
  }
}