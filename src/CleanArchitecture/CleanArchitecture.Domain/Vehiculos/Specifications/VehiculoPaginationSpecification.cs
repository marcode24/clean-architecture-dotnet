using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehiculos.Specifications;

public class VehiculoPaginationSpecification : BaseSpecification<Vehiculo, VehiculoId>
{
  public VehiculoPaginationSpecification(
    string sort,
    int pageIndex,
    int pageSize,
    string search
  ) : base(
    x => string.IsNullOrEmpty(search) || x.Modelo == new Modelo(search)
  )
  {
    ApplyPaging(pageSize * (pageIndex - 1), pageSize);
    if (!string.IsNullOrEmpty(sort))
    {
      switch (sort)
      {
        case "modeloAs": AddOrderBy(x => x.Modelo!); break;
        case "modeloDesc": AddOrderByDescending(x => x.Modelo!); break;
        default: AddOrderBy(x => x.Modelo!); break;
      }
    }
    else
      AddOrderBy(x => x.Modelo!);
  }
}