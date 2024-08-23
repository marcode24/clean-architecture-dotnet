using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Alquileres.ReservarAlquiler;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Alquileres;

public class ReservarAlquilerTests
{
  private readonly ReservarAlquilerCommandHandler _reservarAlquilerCommandHandlerMock;
  private readonly IUserRepository _userRepositoryMock;
  private readonly IVehiculosRepository _vehiculosRepositoryMock;
  private readonly IAlquilerRepository _alquilerRepositoryMock;
  private readonly IUnitOfWork _unitOfWorkMock;
  private readonly DateTime UtcNow = DateTime.UtcNow;
  private readonly ReservarAlquilerCommand _reservarAlquilerCommand = new(
    new VehiculoId(Guid.NewGuid()),
    new UserId(Guid.NewGuid()),
    new DateOnly(2024, 1, 1),
    new DateOnly(2025, 1, 2)
  );

  public ReservarAlquilerTests()
  {
    _userRepositoryMock = Substitute.For<IUserRepository>();
    _vehiculosRepositoryMock = Substitute.For<IVehiculosRepository>();
    _alquilerRepositoryMock = Substitute.For<IAlquilerRepository>();
    _unitOfWorkMock = Substitute.For<IUnitOfWork>();

    IDateTimeProvider dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
    dateTimeProviderMock.currentTime.Returns(UtcNow);

    _reservarAlquilerCommandHandlerMock = new ReservarAlquilerCommandHandler(
      _userRepositoryMock,
      _vehiculosRepositoryMock,
      _alquilerRepositoryMock,
      new PrecioService(),
      _unitOfWorkMock,
      dateTimeProviderMock
    );
  }

  [Fact]
  public async Task Handle_Should_ReturnFailure_WhenUserIsNull()
  {
    _userRepositoryMock
      .GetByIdAsync(_reservarAlquilerCommand.UsuarioId, Arg.Any<CancellationToken>())
      .Returns((User?)null);

    var resultado = await _reservarAlquilerCommandHandlerMock.Handle(_reservarAlquilerCommand, default);

    resultado.Error.Should().Be(UsersErrors.NotFound);
  }
}