using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler;

public record ReservarAlquilerCommand(
  Guid VehiculoId,
  Guid UsuarioId,
  DateOnly FechaInicio,
  DateOnly FechaFin
) : ICommand<Guid>;