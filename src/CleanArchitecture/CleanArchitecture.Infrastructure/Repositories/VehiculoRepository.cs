using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class VehicleRepository : Repository<Vehiculo, VehiculoId>, IVehiculosRepository
{
  public VehicleRepository(ApplicationDbContext dbContext) : base(dbContext) { }

  public Task<int> CountAsync(ISpecification<Vehiculo, VehiculoId> spec)
  {
    throw new NotImplementedException();
  }

  public Task<IReadOnlyList<Vehiculo>> GetAllWithSpec(ISpecification<Vehiculo, VehiculoId> spec)
  {
    throw new NotImplementedException();
  }
}