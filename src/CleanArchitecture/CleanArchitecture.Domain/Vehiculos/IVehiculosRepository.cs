namespace CleanArchitecture.Domain.Vehiculos;

public interface IVehiculosRepository
{
  Task<Vehiculo> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}