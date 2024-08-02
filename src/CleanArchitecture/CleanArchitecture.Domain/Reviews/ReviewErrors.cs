using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews;

public static class ReviewErrors
{
  public static readonly Error NotEligible = new Error(
    "review_not_eligible",
    "The review is not eligible to be created."
  );
}