using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class AlquilerRepository : Repository<Alquiler, AlquilerId>, IAlquilerRepository
{
  private static readonly AlquilerStatus[] ActiveAlquilerStatuses = {
    AlquilerStatus.Reservado,
    AlquilerStatus.Confirmado,
    AlquilerStatus.Completado
  };
  public AlquilerRepository(ApplicationDbContext dbContext) : base(dbContext) { }
  public async Task<bool> IsOverlappingAsync(Vehiculo vehiculo, DateRange duracion, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<Alquiler>()
      .AnyAsync(
        a => a.VehiculoId == vehiculo.Id &&
            a.Duracion!.Inicio <= duracion.Fin &&
            a.Duracion!.Fin >= duracion.Inicio &&
            ActiveAlquilerStatuses.Contains(a.Status),
        cancellationToken
      );
  }
}