using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Infrastructure.Clock;

namespace CleanArchitecture.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
  public DateTime currentTime => DateTime.UtcNow;
}