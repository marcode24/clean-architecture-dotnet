namespace CleanArchitecture.Domain.Abstractions;

public record Error(string Code, string Message)
{
  public static Error None = new(string.Empty, string.Empty);
  public static Error NullValue = new("null_value", "El valor no puede ser nulo.");
}