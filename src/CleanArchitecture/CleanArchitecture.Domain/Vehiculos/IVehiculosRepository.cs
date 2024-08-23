using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehiculos;

public interface IVehiculosRepository
{
  Task<Vehiculo?> GetByIdAsync(VehiculoId id, CancellationToken cancellationToken = default);
  Task<IReadOnlyList<Vehiculo>> GetAllWithSpec(ISpecification<Vehiculo, VehiculoId> spec);
  Task<int> CountAsync(ISpecification<Vehiculo, VehiculoId> spec);
}