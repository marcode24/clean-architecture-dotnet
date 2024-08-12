using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Alquileres.Events;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.UnitTests.Infrastructure;
using CleanArchitecture.Domain.UnitTests.Users;
using CleanArchitecture.Domain.UnitTests.Vehiculos;
using CleanArchitecture.Domain.Users;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Domain.UnitTests.Alquileres;

public class AlquilerTests : BaseTest
{
  [Fact]
  public void Reserva_Should_Raise_AlquilerReservaDomainEvent()
  {
    var user = User.Create(
      UserMock.nombre,
      UserMock.apellido,
      UserMock.email,
      UserMock.password
    );

    var precio = new Moneda(10.0m, TipoMoneda.Usd);
    var duracion = DateRange.Create(
      new DateOnly(2024, 1, 1),
      new DateOnly(2025, 1, 1)
    );

    var vehiculo = VehiculoMock.Create(precio);
    var precioService = new PrecioService();

    var alquiler = Alquiler.Reservar(
      vehiculo,
      user.Id!,
      duracion,
      DateTime.UtcNow,
      precioService
    );

    var alquilerReservaDomainEvent = AssertDomainWasPublished<AlquilerReservadoDomainEvent>(alquiler);

    alquilerReservaDomainEvent.AlquilerId.Should().Be(alquiler.Id);
  }
}