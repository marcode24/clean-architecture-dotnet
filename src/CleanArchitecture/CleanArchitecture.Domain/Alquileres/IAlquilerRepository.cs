using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Alquileres;

public interface IAlquilerRepository
{
  Task<Alquiler> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<bool> IsOverlappingAsync(
    Vehiculo vehiculo,
    DateRange dateRange,
    CancellationToken cancellationToken = default
  );
  void Add(Alquiler alquiler);
}