using System.Net.NetworkInformation;
using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews;

public sealed record Rating
{
  public static readonly Error Invalid = new(
    "rating_invalid",
    "The rating is invalid."
  );
  public int Value { get; init; }
  private Rating(int value) => Value = value;
  public static Result<Rating> Create(int value)
  {
    if (value < 1 || value > 5)
      return Result.Failure<Rating>(Invalid);

    return Result.Success(new Rating(value));
  }
}