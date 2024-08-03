using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class VehicleRepository : Repository<Vehiculo>, IVehiculosRepository
{
  public VehicleRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}