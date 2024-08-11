using System.Data;
using System.Diagnostics;
using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Domain.Abstractions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;

namespace CleanArchitecture.Infrastructure.Outbox;

public record OutboxMessageData(Guid Id, string Content);

[DisallowConcurrentExecution]
internal sealed class InvokeOutboxMessagesJob : IJob
{
  private static readonly JsonSerializerSettings jsonSerializerSettings = new()
  {
    TypeNameHandling = TypeNameHandling.All
  };

  private readonly ISqlConnectionFactory _sqlConnectionFactory;
  private readonly IPublisher _publisher;
  private readonly IDateTimeProvider _dateTimeProvider;
  private readonly OutboxOptions _outboxOptions;
  private readonly ILogger<InvokeOutboxMessagesJob> _logger;

  public InvokeOutboxMessagesJob(
    ISqlConnectionFactory sqlConnectionFactory,
    IPublisher publisher,
    IDateTimeProvider dateTimeProvider,
    IOptions<OutboxOptions> outboxOptions,
    ILogger<InvokeOutboxMessagesJob> logger
  )
  {
    _sqlConnectionFactory = sqlConnectionFactory;
    _publisher = publisher;
    _dateTimeProvider = dateTimeProvider;
    _outboxOptions = outboxOptions.Value;
    _logger = logger;
  }

  public async Task Execute(IJobExecutionContext context)
  {
    _logger.LogInformation("started processing outbox messages");
    using var connection = _sqlConnectionFactory.CreateConnection();
    using var transaction = connection.BeginTransaction();

    var sql = $@"
      SELECT
        id, content
      FROM outbox_messages
      WHERE processed_on_utc IS NULL
      ORDER BY ocurred_on_utc
      LIMIT {_outboxOptions.BatchSize}
      FOR UPDATE
    ";

    var records = (await connection.QueryAsync<OutboxMessageData>(sql, transaction)).ToList();

    foreach (var message in records)
    {
      Exception? exception = null;
      try
      {
        var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content, jsonSerializerSettings)!;
        await _publisher.Publish(domainEvent, context.CancellationToken);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "error processing outbox message with id: {Id}", message.Id);
        exception = ex;
      }
      await UpdateOutboxMessage(connection, transaction, message, exception);
    }

    transaction.Commit();
    _logger.LogInformation("finished processing outbox messages");
  }

  private async Task UpdateOutboxMessage(
    IDbConnection connection,
    IDbTransaction transaction,
    OutboxMessageData message,
    Exception? exception
  )
  {
    const string sql = @"
      UPDATE outbox_messages
      SET
        processed_on_utc = @ProcessedOnUtc,
        error = @Error
      WHERE id = @Id";

    await connection.ExecuteAsync(
      sql,
      new
      {
        message.Id,
        ProcessedOnUtc = _dateTimeProvider.currentTime,
        Error = exception?.ToString()
      },
      transaction
    );
  }
}
