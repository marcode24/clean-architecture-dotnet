using System.Text.Json;
using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CleanArchitecture.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
  // private readonly IPublisher _publisher;
  public static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
  {
    TypeNameHandling = TypeNameHandling.All
  };
  private readonly IDateTimeProvider _dateTimeProvider;

  public ApplicationDbContext(DbContextOptions options, IDateTimeProvider dateTimeProvider) : base(options)
  {
    _dateTimeProvider = dateTimeProvider;
    // _publisher = publisher;
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
      // await PublishDomainEventsAsync();
      AddDomainEventsToOutboxMessages();
      var result = await base.SaveChangesAsync(cancellationToken);

      return result;
    }
    catch (DbUpdateConcurrencyException ex)
    {
      throw new ConcurrencyException("A concurrency error occurred while saving the data", ex);
    }
  }

  private void AddDomainEventsToOutboxMessages()
  {
    var outboxMessages = ChangeTracker
      .Entries<IEntity>()
      .Select(e => e.Entity)
      .SelectMany(e =>
      {
        var domainEvents = e.GetDomainEvents();
        e.ClearDomainEvents();
        return domainEvents;
      })
      .Select(domainEvent => new OutboxMessage(
        Guid.NewGuid(),
        _dateTimeProvider.currentTime,
        domainEvent.GetType().Name,
        JsonConvert.SerializeObject(domainEvent, jsonSerializerSettings)
      ))
      .ToList();

    // foreach (var domainEvent in domainEvents)
    // {
    //   await _publisher.Publish(domainEvent);
    // }}

    AddRange(outboxMessages);
  }
}