using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehiculos;

public static class VehiculoErrors
{
  public static readonly Error NotFound = new(
    "vehiculo_not_found",
    "The vehiculo was not found."
  );
}