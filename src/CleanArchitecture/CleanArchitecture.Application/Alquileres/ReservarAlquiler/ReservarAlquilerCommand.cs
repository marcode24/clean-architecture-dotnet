using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler;

public record ReservarAlquilerCommand(
  VehiculoId VehiculoId,
  UserId UsuarioId,
  DateOnly FechaInicio,
  DateOnly FechaFin
) : ICommand<Guid>;