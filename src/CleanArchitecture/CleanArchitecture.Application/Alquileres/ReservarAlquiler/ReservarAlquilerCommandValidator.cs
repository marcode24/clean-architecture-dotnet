using FluentValidation;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler;

public class ReservarAlquilerCommandValidator : AbstractValidator<ReservarAlquilerCommand>
{
  public ReservarAlquilerCommandValidator()
  {
    RuleFor(c => c.UsuarioId).NotEmpty();
    RuleFor(c => c.VehiculoId).NotEmpty();
    RuleFor(c => c.FechaInicio).LessThan(c => c.FechaFin);
  }
}