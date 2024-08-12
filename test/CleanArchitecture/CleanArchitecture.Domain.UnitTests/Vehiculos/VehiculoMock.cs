using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.UnitTests.Vehiculos;

internal static class VehiculoMock
{
  public static Vehiculo Create(Moneda precio, Moneda? mantenimiento = null) => new(
      VehiculoId.New(),
      new Modelo("Civic"),
      new Vin("1HGCM826"),
      new Direccion("USA", "test", "test", "test", "test"),
      precio,
      mantenimiento ?? Moneda.Zero(),
      DateTime.UtcNow.AddYears(-1),
      []
    );
}