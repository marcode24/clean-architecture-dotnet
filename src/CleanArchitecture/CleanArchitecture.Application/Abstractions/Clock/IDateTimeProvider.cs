namespace CleanArchitecture.Application.Abstractions.Clock;

public interface IDateTimeProvider
{
  DateTime currentTime { get; }
}