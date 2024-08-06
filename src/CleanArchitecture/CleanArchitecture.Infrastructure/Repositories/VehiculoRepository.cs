using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class VehicleRepository : Repository<Vehiculo, VehiculoId>, IVehiculosRepository
{
  public VehicleRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}