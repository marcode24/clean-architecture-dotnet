namespace CleanArchitecture.Domain.Vehiculos;

public interface IVehiculosRepository
{
  Task<Vehiculo> GetByIdAsync(VehiculoId id, CancellationToken cancellationToken = default);
}