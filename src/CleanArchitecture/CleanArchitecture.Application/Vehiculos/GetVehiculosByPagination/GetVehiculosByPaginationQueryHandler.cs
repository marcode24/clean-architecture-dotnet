using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Application.Vehiculos.GetVehiculosByPagination;

internal sealed class GetVehiculosByPaginationQueryHandler() : IQueryHandler<GetVehiculosByPaginationQuery, PaginationResult<Vehiculo, VehiculoId>>
{
  private readonly IVehiculosRepository? _vehiculosRepository;

  public GetVehiculosByPaginationQueryHandler(IVehiculosRepository vehiculosRepository): this()
  {
    _vehiculosRepository = vehiculosRepository;
  }

  public Task<Result<PaginationResult<Vehiculo, VehiculoId>>> Handle(GetVehiculosByPaginationQuery request, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}