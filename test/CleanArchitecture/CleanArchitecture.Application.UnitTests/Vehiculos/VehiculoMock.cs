using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Application.UnitTests.Vehiculos;

internal static class VehiculoMock
{
  public static Vehiculo Create() => new(
    new VehiculoId(Guid.NewGuid()),
    new Modelo("Modelo"),
    new Vin("Vin"),
    new Direccion("Direccion", "Ciudad", "Pais", "Ciudad", "Calle"),
    new Moneda(150.0m, TipoMoneda.Usd),
    Moneda.Zero(),
    DateTime.UtcNow,
    []
  );
}